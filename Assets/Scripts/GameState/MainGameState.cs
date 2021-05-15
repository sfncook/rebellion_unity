using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class MainGameState : MonoBehaviour
{
    public static MainGameState gameState;

    [HideInInspector]
    public readonly List<Planet> planets = new List<Planet>();
    [HideInInspector]
    public int gameTime = 0;
    [HideInInspector]
    public bool isTimerRunning = false;
    [HideInInspector]
    public Team playerTeam = Team.TeamA;

    // Game Loop Events
    [HideInInspector]
    public UnityEvent uiUpdateEvent = new UnityEvent();     // 1. UI is updated
                                                            // 2. Pause waiting for user action
    [HideInInspector]
    public UnityEvent agentPlanEvent = new UnityEvent();    // 3. Pieces make decisions where to move
    [HideInInspector]
    public UnityEvent agentActionEvent = new UnityEvent();  // 4. Battles takes place, pieces take damage
    [HideInInspector]
    public UnityEvent postCleanupEvent = new UnityEvent();  // 5. Dead pieces are removed and gameState updated in response to decisions

    // Planet Dialog
    [HideInInspector]
    public Planet planetForDetail;

    void Awake()
    {
        if(gameState == null) {
            DontDestroyOnLoad(gameObject);
            gameState = this;
            initializeGameState();
        }
        else if(gameState != this)
        {
            Destroy(gameObject);
        }
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

    public void gameUpdateNotification()
    {
        uiUpdateEvent.Invoke();
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
                Team team = (Random.Range(0.0f,1.0f)>=0.5f) ? Team.TeamA : Team.TeamB;
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
            for (var i = 0; i < Random.Range(0, 3); i++)
            {
                Team team = (Random.Range(0.0f, 1.0f) >= 0.5f) ? Team.TeamA : Team.TeamB;
                int typeIndex = Random.Range(0, personnelTypes.Length);
                Personnel personnel = new Personnel(personnelTypes[typeIndex], team);
                planet.personnelsOnSurface.Add(personnel);
            }
        }
    }
}
