using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainGameState : MonoBehaviour
{
    private const float SEC_PER_GAMEDAY = 1.0f;

    public static MainGameState gameState;
    public TextAsset galaxyDataFile;

    [HideInInspector]
    public readonly Galaxy galaxy = new Galaxy();

    [HideInInspector]
    public readonly List<Planet> planets = new List<Planet>();
    [HideInInspector]
    public int gameTime = 1;
    [HideInInspector]
    private bool isTimerRunning = false;
    [HideInInspector]
    public Team myTeam = Team.TeamA;
    [HideInInspector]
    public UnityEvent startTimerEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent stopTimerEvent = new UnityEvent();

    // Game Loop Events
                                                            // 1. - Game time is incremented
    [HideInInspector]
    public UnityEvent preDayPrepEvent = new UnityEvent();   // 2. - Factory builds complete
    [HideInInspector]
    public UnityEvent agentPlanEvent = new UnityEvent();    // 3. - Pieces make decisions where to move
    [HideInInspector]
    public UnityEvent agentActionEvent = new UnityEvent();  // 4. - Battles takes place
                                                            //    - pieces take damage
    [HideInInspector]
    public UnityEvent postCleanupEvent = new UnityEvent();  // 5. - Dead pieces are removed
                                                            //    - units arrive at destinations
                                                            //    - gameState updated in response to decisions
                                                            //    - unit-arrivals are processed
    [HideInInspector]
    public UnityEvent uiUpdateEvent = new UnityEvent();     // 6. - UI is updated
                                                            // 7. - Pause waiting for user action

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


    // Story-line state
    [HideInInspector]
    public bool newGameFadeIn = false;
    [HideInInspector]
    public bool showPlanetDetailBackButton = false;

    private float timerSec = 0.0f;

    // Updaters
    private AllShipsUpdater allShipsUpdater = new AllShipsUpdater();
    private AllPersonnelUpdater allPersonnelUpdater = new AllPersonnelUpdater();
    private AllDefenseUpdater allDefenseUpdater = new AllDefenseUpdater();
    private AllFactoriesUpdater allFactoriesUpdater = new AllFactoriesUpdater();

    void Awake()
    {
        if(gameState == null) {
            DontDestroyOnLoad(gameObject);
            gameState = this;

            allShipsUpdater.init();
            allPersonnelUpdater.init();
            allDefenseUpdater.init();
            allFactoriesUpdater.init();
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


    public void removeListenerGameStateUpdateEvent(UnityAction call)
    {
        uiUpdateEvent.RemoveListener(call);
    }


    public void invokeUiUpdateEvent()
    {
        uiUpdateEvent.Invoke();
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
        loadGameFromFiles();

        // Randomly pick starting sector and planet:
        StarSector homeSector = galaxy.sectors[Random.Range(0, galaxy.sectors.Count)];
        Planet homePlanet = homeSector.planets[Random.Range(0, homeSector.planets.Count)];
        homePlanet.loyalty = 0.4f;
        initPlanetUnits(homePlanet);
        homePlanet.energyCapacity = 5;

        // Init standard set of intro units
        homePlanet.factories.Add(new Factory(FactoryType.ctorYard));
        homePlanet.defenses.Add(new Defense(DefenseType.planetaryShield));
        homePlanet.personnelsOnSurface.Add(new Personnel(PersonnelType.Soldiers, Team.TeamB));

        gameState.sectorForDetail = homeSector;
        gameState.planetForDetail = homePlanet;
        gameState.newGameFadeIn = true;
        gameState.startTimerEvent.Invoke();
        SceneManager.LoadScene("Planet Detail 2");
    }


    public void loadGameFromFiles()
    {
        JsonUtility.FromJsonOverwrite(galaxyDataFile.text, galaxy);

        foreach(StarSector sector in galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                initPlanetUnits(planet);
                planet.energyCapacity = Random.Range(0, 10);
                initPlanet(planet);
            }
        }
    }// loadGameFromFiles

    private void initPlanetUnits(Planet planet)
    {
        planet.shipsInOrbit = new List<Ship>();
        planet.shipsInTransit = new List<Ship>();
        planet.personnelsOnSurface = new List<Personnel>();
        planet.personnelsInTransit = new List<Personnel>();
        planet.factories = new List<Factory>();
        planet.factoriesInTransit = new List<Factory>();
        planet.defenses = new List<Defense>();
        planet.defensesInTransit = new List<Defense>();
    }

    private void initPlanet(Planet planet)
    {
        // Initializing everything to enemy strength
        Team enemyTeam = Team.TeamB;
        var shipTypes = new ShipType[] {
            ShipType.Bireme,
            ShipType.Trireme,
            ShipType.Quadreme,
            ShipType.Quintreme
        };

        for (var i = 0; i < Random.Range(0, 3); i++)
        {
            //Team team = (Random.Range(0.0f, 1.0f) >= 0.5f) ? Team.TeamA : Team.TeamB;
            int typeIndex = Random.Range(0, shipTypes.Length);
            Ship randShip = new Ship(shipTypes[typeIndex], enemyTeam);
            planet.shipsInOrbit.Add(randShip);
        }


        var personnelTypes = new PersonnelType[] {
            PersonnelType.Diplomat,
            PersonnelType.Soldiers,
            PersonnelType.Spy
        };
        for (var i = 0; i < Random.Range(0, 4); i++)
        {
            //Team team = (Random.Range(0.0f, 1.0f) >= 0.5f) ? Team.TeamA : Team.TeamB;
            //int typeIndex = Random.Range(0, personnelTypes.Length);
            //Personnel personnel = new Personnel(personnelTypes[typeIndex], team);
            Personnel personnel = new Personnel(PersonnelType.Soldiers, enemyTeam);
            planet.personnelsOnSurface.Add(personnel);
        }

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

        planet.loyalty = Random.Range(0.0f, 0.999f);
    }

    private static StarSector findSectorForPlanet(Planet planet)
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
}
