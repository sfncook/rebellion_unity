using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

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
    public int gameTime = 0;
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
            //initializeGameState();
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


    public void initializeGameState()
    {
        Planet ater = new Planet("Ater", Random.Range(1, 10), Random.Range(0.0f, 0.999f));
        Planet nageron = new Planet("Nageron", Random.Range(1, 10), Random.Range(0.0f, 0.999f));
        Planet ucholla = new Planet("Ucholla", Random.Range(1, 10), Random.Range(0.0f, 0.999f));
        Planet obiemia = new Planet("Obiemia", Random.Range(1, 10), Random.Range(0.0f, 0.999f));
        Planet ibos = new Planet("Ibos", Random.Range(1, 10), Random.Range(0.0f, 0.999f));
        planets.Add(ater);
        planets.Add(nageron);
        planets.Add(ucholla);
        planets.Add(obiemia);
        planets.Add(ibos);

        // Pick Team HQ Planets
        List<Planet> planetsCopy = planets.GetRange(0, planets.Count);
        int teamAHq = Random.Range(0, planetsCopy.Count);
        Planet planetTeamA = planetsCopy[teamAHq];
        planetsCopy.Remove(planetTeamA);
        int teamBHq = Random.Range(0, planetsCopy.Count);
        Planet planetTeamB = planetsCopy[teamBHq];

        planetTeamA.isHq = true;
        planetTeamB.isHq = true;

        planetTeamA.loyalty = 0.1f;
        planetTeamB.loyalty = 0.9f;

        Ship initShipA = new Ship(ShipType.Bireme, Team.TeamA);
        Ship initShipB = new Ship(ShipType.Bireme, Team.TeamB);

        planetTeamA.shipsInOrbit.Add(initShipA);
        planetTeamB.shipsInOrbit.Add(initShipB);

        planetTeamA.factories.Add(new Factory(FactoryType.ctorYard));
        planetTeamB.factories.Add(new Factory(FactoryType.ctorYard));


        // Randomly add ships
        var shipTypes = new ShipType[] {
            ShipType.Bireme,
            ShipType.Trireme,
            ShipType.Quadreme,
            ShipType.Quintreme
        };
        foreach (var planet in planets)
        {
            for(var i=0; i<Random.Range(0,3); i++)
            {
                Team team = (Random.Range(0.0f, 1.0f) >= 0.5f) ? Team.TeamA : Team.TeamB;
                int typeIndex = Random.Range(0, shipTypes.Length);
                Ship randShip = new Ship(shipTypes[typeIndex], team);
                planet.shipsInOrbit.Add(randShip);
            }
        }

        // Randomly add personnel
        var personnelTypes = new PersonnelType[] {
            PersonnelType.Diplomat,
            PersonnelType.Soldiers,
            PersonnelType.Spy
        };
        foreach (var planet in planets)
        {
            for (var i = 0; i < Random.Range(0, 4); i++)
            {
                Team team = (Random.Range(0.0f, 1.0f) >= 0.5f) ? Team.TeamA : Team.TeamB;
                //int typeIndex = Random.Range(0, personnelTypes.Length);
                //Personnel personnel = new Personnel(personnelTypes[typeIndex], team);
                Personnel personnel = new Personnel(PersonnelType.Soldiers, team);
                planet.personnelsOnSurface.Add(personnel);
            }
        }

        // Randomly add factories
        var factoryTypes = new FactoryType[] {
            FactoryType.ctorYard,
            FactoryType.shipYard,
            FactoryType.trainingFac
        };
        foreach (var planet in planets)
        {
            int many = Mathf.Min(Random.Range(0, 3), (planet.energyCapacity - planet.factories.Count - planet.defenses.Count));
            for (var i = 0; i < many; i++)
            {
                int typeIndex = Random.Range(0, factoryTypes.Length);
                Factory factory = new Factory(factoryTypes[typeIndex]);
                planet.factories.Add(factory);
            }
        }

        // Randomly add defenses
        var defenseTypes = new DefenseType[] {
            DefenseType.orbitalBattery,
            DefenseType.planetaryShield
        };
        foreach (var planet in planets)
        {
            int many = Mathf.Min(Random.Range(0, 3), (planet.energyCapacity - planet.factories.Count - planet.defenses.Count));
            for (var i = 0; i < many; i++)
            {
                int typeIndex = Random.Range(0, defenseTypes.Length);
                Defense defense = new Defense(defenseTypes[typeIndex]);
                bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
                if(defense.type.Equals(DefenseType.orbitalBattery) || !hasOrbitalShield)
                {
                    planet.defenses.Add(defense);
                }
            }
        }
    }// initializeGameState

    public void loadGameFromFiles()
    {
        JsonUtility.FromJsonOverwrite(galaxyDataFile.text, galaxy);

        foreach(StarSector sector in galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                planet.shipsInOrbit = new List<Ship>();
                planet.shipsInTransit = new List<Ship>();
                planet.personnelsOnSurface = new List<Personnel>();
                planet.personnelsInTransit = new List<Personnel>();
                planet.factories = new List<Factory>();
                planet.factoriesInTransit = new List<Factory>();
                planet.defenses = new List<Defense>();
                planet.defensesInTransit = new List<Defense>();
                planet.energyCapacity = Random.Range(0, 10);
                initPlanet(planet);
            }
        }

        StarSector sectorA = galaxy.sectors[Random.Range(0, galaxy.sectors.Count)];
        StarSector sectorB = galaxy.sectors[Random.Range(0, galaxy.sectors.Count)];
        Planet planetA = sectorA.planets[Random.Range(0, sectorA.planets.Count)];
        Planet planetB = sectorB.planets[Random.Range(0, sectorB.planets.Count)];
        planetA.isHq = true;
        planetB.isHq = true;
        planetA.loyalty = 0.1f;
        planetB.loyalty = 0.9f;
    }// loadGameFromFiles

    private void initPlanet(Planet planet)
    {
        var shipTypes = new ShipType[] {
            ShipType.Bireme,
            ShipType.Trireme,
            ShipType.Quadreme,
            ShipType.Quintreme
        };

        for (var i = 0; i < Random.Range(0, 3); i++)
        {
            Team team = (Random.Range(0.0f, 1.0f) >= 0.5f) ? Team.TeamA : Team.TeamB;
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
            Team team = (Random.Range(0.0f, 1.0f) >= 0.5f) ? Team.TeamA : Team.TeamB;
            //int typeIndex = Random.Range(0, personnelTypes.Length);
            //Personnel personnel = new Personnel(personnelTypes[typeIndex], team);
            Personnel personnel = new Personnel(PersonnelType.Soldiers, team);
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
            bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
            if (defense.type.Equals(DefenseType.orbitalBattery) || !hasOrbitalShield)
            {
                planet.defenses.Add(defense);
            }
        }

        planet.loyalty = Random.Range(0.0f, 0.999f);
    }
}
