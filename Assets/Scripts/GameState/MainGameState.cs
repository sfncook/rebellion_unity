using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

public class MainGameState : MonoBehaviour
{
    private const float SEC_PER_GAMEDAY = 1.0f;

    public static MainGameState gameState;
    public TextAsset galaxyDataFile;

    private bool gameInitialized = false;

    [HideInInspector]
    public readonly Galaxy galaxy = new Galaxy();

    [HideInInspector]
    public List<Hero> heroesAvailableForRecruiting = new List<Hero>();

    [HideInInspector]
    public readonly List<Planet> planets = new List<Planet>();
    [HideInInspector]
    public int gameTime = 1;
    [HideInInspector]
    private bool isTimerRunning = false;
    [HideInInspector]
    public Team myTeam = Team.TeamA;
    public Team enemyTeam = Team.TeamB;
    [HideInInspector]
    public UnityEvent startTimerEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent stopTimerEvent = new UnityEvent();

    [HideInInspector]
    public UnityEvent<FilterType> filterChangeEvent = new UnityEvent<FilterType>();
    public FilterType selectedFilterType = FilterType.PlanetsLoyalty;

    // Game Loop Events
    // 1. - Game time is incremented
    [HideInInspector]
    public UnityEvent preDayPrepEvent = new UnityEvent();       // 2. - Factory builds complete
                                                                //    - Missions complete
    [HideInInspector]
    public UnityEvent agentPlanEvent = new UnityEvent();        // 4. - Pieces make decisions where to move
    [HideInInspector]
    public UnityEvent agentActionEvent = new UnityEvent();      // 5. - Battles takes place
                                                                //    - pieces take damage
    [HideInInspector]
    public UnityEvent postCleanupEvent = new UnityEvent();      // 6. - Dead pieces are removed
                                                                //    - units arrive at destinations
                                                                //    - gameState updated in response to decisions
                                                                //    - unit-arrivals are processed
    [HideInInspector]
    public UnityEvent storyLineUpdateEvent = new UnityEvent();  // 7. - Update any storyline elements
    [HideInInspector]
    public UnityEvent uiUpdateEvent = new UnityEvent();         // 8. - UI is updated
                                                                //    - Pause waiting for user action

    // Sector Map
    [HideInInspector]
    public StarSector sectorForDetail;

    // Planet Dialog
    [HideInInspector]
    public Planet planetForDetail;

    // Factory Dialog(s)
    [HideInInspector]
    public Factory factoryForDetail;
    [HideInInspector]
    public Planet planetSelectedForDestination;

    [HideInInspector]
    public Personnel personnelForDetail;


    // Story-line state
    [HideInInspector]
    public bool newGameFadeIn = false;
    [HideInInspector]
    public bool showPlanetDetailBackButton = true;
    [HideInInspector]
    public Planet homePlanet = null;
    [HideInInspector]
    public bool firstRecruitingTask = true;

    private float timerSec = 0.0f;


    [HideInInspector]
    public Report reportForDialog = null;

    [HideInInspector]
    public List<Report> reportsShowImmediately = new List<Report>();
    [HideInInspector]
    public List<Report> reportsUnAcked = new List<Report>();
    [HideInInspector]
    public List<Report> reportsAcked = new List<Report>();

    [HideInInspector]
    public UnityEvent<Personnel, AbstractUnit, Planet> showMissionAssignmentDialog = new UnityEvent<Personnel, AbstractUnit, Planet>();

    [HideInInspector]
    readonly SortedDictionary<int, List<string>> gameEventsByTime = new SortedDictionary<int, List<string>>();


    // Updaters
    private AllPlanetsUpdater allPlanetsUpdater = new AllPlanetsUpdater();
    private AllShipsUpdater allShipsUpdater = new AllShipsUpdater();
    private AllPersonnelUpdater allPersonnelUpdater = new AllPersonnelUpdater();
    private AllDefenseUpdater allDefenseUpdater = new AllDefenseUpdater();
    private AllFactoriesUpdater allFactoriesUpdater = new AllFactoriesUpdater();
    //private AllStoryLineUpdater allStoryLineUpdater = new AllStoryLineUpdater();
    private AllMissionsUpdater allMissionsUpdater = new AllMissionsUpdater();
    private WinLoseUpdater winLoseUpdater = new WinLoseUpdater();
    private AiAgentPlanUpdater aiAgentPlanUpdater = new AiAgentPlanUpdater();


    void Awake()
    {
        if(gameState == null) {
            DontDestroyOnLoad(gameObject);
            gameState = this;

            allPlanetsUpdater.init();
            allShipsUpdater.init();
            allPersonnelUpdater.init();
            allDefenseUpdater.init();
            allFactoriesUpdater.init();
            //allStoryLineUpdater.init();
            allMissionsUpdater.init();
            winLoseUpdater.init();
            aiAgentPlanUpdater.init();
        }
        else if(gameState != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startTimerEvent.AddListener(onStartTimer);
        stopTimerEvent.AddListener(onStopTimer);

        addListenerUiUpdateEvent(updateReports);
    }

    private void Update()
    {
        if (gameState.getIsTimerRunning())
        {
            if (timerSec <= 0.0f)
            {
                ++gameState.gameTime;
                timerSec = SEC_PER_GAMEDAY;

                gameState.invokePreDayPrepEvent();
                gameState.invokeAgentPlanEvent();
                gameState.invokeAgentActionEvent();
                gameState.invokePostCleanupEvent();
                gameState.invokeStoryLineUpdateEvent();
                gameState.invokeUiUpdateEvent();
            }
            else
            {
                timerSec -= Time.deltaTime;
            }
        }
        else
        {
            // Reset timer
            timerSec = SEC_PER_GAMEDAY;
        }
    }// Update

    public void addGameEvent(string eventMessage)
    {
        List<string> eventMsgs;
        try
        {
            eventMsgs = gameEventsByTime[gameTime];
        }
        catch (KeyNotFoundException)
        {
            eventMsgs = new List<string>();
            gameEventsByTime[gameTime] = eventMsgs;
        }
        eventMsgs.Add(eventMessage);
    }

    public List<string> getEventsForTime(int gameTime)
    {
        List<string> eventMsgs;
        try
        {
            eventMsgs = gameEventsByTime[gameTime];
        }
        catch (KeyNotFoundException)
        {
            eventMsgs = new List<string>();
            gameEventsByTime[gameTime] = eventMsgs;
        }
        return eventMsgs;
    }

    public void setNewFilter(FilterType filterType)
    {
        selectedFilterType = filterType;
        filterChangeEvent.Invoke(filterType);
    }

    public Planet getPlanetByName(string planetName)
    {
        foreach (Planet planet in planets)
        {
            if (planet.name.Equals(planetName))
            {
                return planet;
            }
        }
        return null;
    }

    public void addListenerUiUpdateEvent(UnityAction call)
    {
        uiUpdateEvent.AddListener(call);
    }

    public void addPreDayPrepEvent(UnityAction call)
    {
        preDayPrepEvent.AddListener(call);
    }

    public void addListenerAgentPlanEvent(UnityAction call)
    {
        agentPlanEvent.AddListener(call);
    }

    public void addListenerAgentActionEvent(UnityAction call)
    {
        agentActionEvent.AddListener(call);
    }

    public void addListenerPostCleanupEvent(UnityAction call)
    {
        postCleanupEvent.AddListener(call);
    }

    public void addListenerStoryLineUpdateEvent(UnityAction call)
    {
        storyLineUpdateEvent.AddListener(call);
    }


    public void removeListenerGameStateUpdateEvent(UnityAction call)
    {
        uiUpdateEvent.RemoveListener(call);
    }


    public void invokeUiUpdateEvent()
    {
        uiUpdateEvent.Invoke();
    }

    public void invokeStoryLineUpdateEvent()
    {
        storyLineUpdateEvent.Invoke();
    }

    public void invokePreDayPrepEvent()
    {
        preDayPrepEvent.Invoke();
    }

    public void invokeAgentPlanEvent()
    {
        agentPlanEvent.Invoke();
    }

    public void invokeAgentActionEvent()
    {
        agentActionEvent.Invoke();
    }

    public void invokePostCleanupEvent()
    {
        postCleanupEvent.Invoke();
    }

    private void onStartTimer()
    {
        gameState.isTimerRunning = true;
    }

    private void onStopTimer()
    {
        gameState.isTimerRunning = false;
    }

    public bool getIsTimerRunning()
    {
        return isTimerRunning;
    }



    public void initializeNewGame()
    {
        if(!gameInitialized)
        {
            gameInitialized = true;

            loadGameFromFiles();

            var sectorsToPickHqsFrom = galaxy.sectors.GetRange(0, galaxy.sectors.Count);

            // Init Team A HQ
            var teamAHomeSector = sectorsToPickHqsFrom[Random.Range(0, sectorsToPickHqsFrom.Count)];
            sectorsToPickHqsFrom.Remove(teamAHomeSector);
            var teamAHomePlanet = teamAHomeSector.planets[Random.Range(0, teamAHomeSector.planets.Count)];
            teamAHomePlanet.personnelsOnSurface.Add(new Personnel(PersonnelType.ChosenOne, Team.TeamA));
            teamAHomePlanet.loyalty = Random.Range(0.9f, 0.999f);
            initPlanet(teamAHomePlanet, Team.TeamA);
            teamAHomePlanet.energyCapacity = (int)Mathf.Max(5f, teamAHomePlanet.energyCapacity);
            teamAHomePlanet.factories.Add(new Factory(FactoryType.ctorYard));
            teamAHomePlanet.factories.Add(new Factory(FactoryType.shipYard));
            teamAHomePlanet.defenses.Add(new Defense(DefenseType.planetaryShield));


            // Init Team B HQ
            var teamBHomeSector = sectorsToPickHqsFrom[Random.Range(0, sectorsToPickHqsFrom.Count)];
            sectorsToPickHqsFrom.Remove(teamBHomeSector);
            var teamBHomePlanet = teamBHomeSector.planets[Random.Range(0, teamBHomeSector.planets.Count)];
            teamBHomePlanet.shipsInOrbit.Add(new Ship(ShipType.Capitol, Team.TeamB));
            foreach(Planet planet in teamBHomeSector.planets)
            {
                planet.loyalty = Random.Range(0.0f, 0.7f);
                initPlanet(planet, Team.TeamB);
            }
            teamBHomePlanet.loyalty = Random.Range(0.0f, 0.3f);

            Debug.Log("Team A:"+ teamAHomeSector.name+" "+ teamAHomePlanet.name);
            Debug.Log("Team B:"+ teamBHomeSector.name+" "+ teamBHomePlanet.name);
        }
    }


    public void loadGameFromFiles()
    {
        JsonUtility.FromJsonOverwrite(galaxyDataFile.text, galaxy);

        foreach (StarSector sector in galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                initPlanetUnits(planet);
                planet.energyCapacity = Random.Range(0, 10);
                planet.loyalty = Random.Range(0.45f, 0.55f);
            }
        }
    }// loadGameFromFiles

    private void initPlanetUnits(Planet planet)
    {
        planet.shipsInOrbit = new List<Ship>();
        planet.shipsInTransit = new List<Ship>();
        planet.shipsToDeploy = new List<Ship>();
        planet.personnelsOnSurface = new List<Personnel>();
        planet.personnelsInTransit = new List<Personnel>();
        planet.personnelsToDeploy = new List<Personnel>();
        planet.factories = new List<Factory>();
        planet.factoriesInTransit = new List<Factory>();
        planet.factoriesToDeploy = new List<Factory>();
        planet.defenses = new List<Defense>();
        planet.defensesInTransit = new List<Defense>();
        planet.defensesToDeploy = new List<Defense>();
    }

    private void initPlanet(Planet planet, Team team)
    {
        planet.setDiscoveredByTeam(team);
        var shipTypes = new ShipType[] {
            ShipType.Bireme,
            ShipType.Trireme,
            ShipType.Quadreme,
            ShipType.Quintreme
        };

        for (var i = 0; i < Random.Range(0, 3); i++)
        {
            int typeIndex = Random.Range(0, shipTypes.Length);
            Ship randShip = new Ship(shipTypes[typeIndex], team);
            planet.shipsInOrbit.Add(randShip);
        }


        var personnelTypes = new PersonnelType[] {
            PersonnelType.Diplomat,
            PersonnelType.Soldiers,
            PersonnelType.Spy
        };
        for (var i = 0; i < Random.Range(0, 4); i++)
        {
            Personnel personnel = new Personnel(PersonnelType.Soldiers, team);
            planet.personnelsOnSurface.Add(personnel);
        }

        if(team == Team.TeamB)
        {
            var factoryTypes = new FactoryType[] {
            FactoryType.ctorYard,
            FactoryType.shipYard,
            FactoryType.trainingFac
        };
            int many = Mathf.Min(Random.Range(0, 3), (planet.energyCapacity - planet.factories.Count - planet.defenses.Count));
            for (var i = 0; i < many; i++)
            {
                int typeIndex = Random.Range(0, factoryTypes.Length);
                Factory factory = new Factory(factoryTypes[typeIndex]);
                planet.factories.Add(factory);
            }
        }

        var defenseTypes = new DefenseType[] {
            DefenseType.orbitalBattery,
            DefenseType.planetaryShield
        };
        int many2 = Mathf.Min(Random.Range(0, 3), (planet.energyCapacity - planet.factories.Count - planet.defenses.Count));
        for (var i = 0; i < many2; i++)
        {
            int typeIndex = Random.Range(0, defenseTypes.Length);
            Defense defense = new Defense(defenseTypes[typeIndex]);
            // Limit each planet to just a single orbital shield
            bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
            if (defense.type.Equals(DefenseType.orbitalBattery) || !hasOrbitalShield)
            {
                planet.defenses.Add(defense);
            }
        }

        //planet.loyalty = Random.Range(0.0f, 0.999f);
    }

    public static StarSector findSectorForPlanet(Planet planet)
    {
        foreach (StarSector sector in MainGameState.gameState.galaxy.sectors)
        {
            foreach (Planet _planet in sector.planets)
            {
                if (_planet.Equals(planet))
                {
                    return sector;
                }
            }
        }
        return null;
    }

    public static Planet findPlanetForPersonnel(Personnel personnel)
    {
        foreach (StarSector sector in MainGameState.gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                foreach (Personnel _personnel in planet.personnelsOnSurface)
                {
                    if(_personnel.Equals(personnel))
                    {
                        return planet;
                    }
                }
            }   
        }
        return null;
    }

    public static Planet findPlanetForFactory(Factory factory)
    {
        foreach (StarSector sector in MainGameState.gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                if(planet.factories.Find(f => f.Equals(factory)) != null)
                {
                    return planet;
                }
            }   
        }
        return null;
    }

    public static int arrivalDay(Planet src, Planet dst)
    {
        StarSector srcSector = findSectorForPlanet(src);
        StarSector dstSector = findSectorForPlanet(dst);

        float dx;
        float dy;
        float multiplier;
        if(srcSector.Equals(dstSector))
        {
            dx = src.sectorX - dst.sectorX;
            dy = src.sectorY - dst.sectorY;
            multiplier = 10;
        } else
        {

            dx = srcSector.galaxyX - dstSector.galaxyX;
            dy = srcSector.galaxyY - dstSector.galaxyY;
            multiplier = 50;
        }

        float d = Mathf.Sqrt((dx * dx) + (dy * dy));
        float dm = d * multiplier;
        int dmi = (int)dm;
        //Debug.Log("dx:"+dx+" dy:"+dy+" d:"+d+" dm:"+dm+" dmi:"+dmi);
        return dmi + MainGameState.gameState.gameTime;
    }

    public void ackReport(Report report)
    {
        MainGameState.gameState.reportsUnAcked.Remove(report);
        MainGameState.gameState.reportsAcked.Add(report);
    }

    // Check for any unack'd reports marked as showImmediately
    private void updateReports()
    {
        // Auto-ack all reports over 5 days old
        List<Report> oldUnackedReports = gameState.reportsUnAcked.FindAll(r => (MainGameState.gameState.gameTime - r.dayComplete) >= 5);
        gameState.reportsUnAcked = gameState.reportsUnAcked.Except(oldUnackedReports).ToList();
        gameState.reportsAcked.AddRange(oldUnackedReports);

        if(MainGameState.gameState.reportsShowImmediately.Count > 0)
        {
            MainGameState.gameState.reportForDialog = MainGameState.gameState.reportsShowImmediately[0];
            MainGameState.gameState.reportsShowImmediately.RemoveAt(0);
            SceneManager.LoadScene("Info Report Dialog");
        }

        foreach (Report report in gameState.reportsUnAcked)
        {
            if(report.showImmediately)
            {
                MainGameState.gameState.reportForDialog = report;
                SceneManager.LoadScene(report.dialogScene);
                return;
            }
        }
    }

    public void startTimer()
    {
        startTimerEvent.Invoke();
    }

    public void stopTimer()
    {
        stopTimerEvent.Invoke();
    }

    public void playerWins()
    {
        Debug.Log("Player Wins!");
        stopTimer();
    }

    public void playerLoses()
    {
        Debug.Log("Player Loses!");
        stopTimer();
    }

    public void factoryBuildOrder(Factory factory, AbstractType buildType, Planet planetDestination)
    {
        factory.isBuilding = true;
        factory.buildingType = buildType;
        factory.buildingDoneDay = gameTime + buildType.daysToBuild;
        factory.planetDestination = planetDestination;
    }
}
