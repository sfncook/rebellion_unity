using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class MainGameState : MonoBehaviour
{
    public static MainGameState gameState;

    public List<Planet> planets;
    public int gameTime = 0;
    public bool isTimerRunning = false;

    // Game Loop Events
    public UnityEvent uiUpdateEvent = new UnityEvent();     // 1. UI is updated
                                                            // 2. Pause waiting for user action
    public UnityEvent agentPlanEvent = new UnityEvent();    // 3. Pieces make decisions where to move
    public UnityEvent agentActionEvent = new UnityEvent();  // 4. Battles takes place, pieces take damage
    public UnityEvent postCleanupEvent = new UnityEvent();  // 5. Dead pieces are removed and gameState updated in response to decisions

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
        Planet teamAhq = planetsCopy[teamAHq];
        planetsCopy.Remove(teamAhq);
        int teamBHq = Random.Range(0, planetsCopy.Count);
        Planet teamBhq = planetsCopy[teamBHq];

        Ship initShipA = new Ship(ShipType.Bireme, Team.TeamA);
        Ship initShipB = new Ship(ShipType.Bireme, Team.TeamB);

        teamAhq.shipsInOrbit.Add(initShipA);
        teamBhq.shipsInOrbit.Add(initShipB);
    }
}
