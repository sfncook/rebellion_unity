using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class MainGameState : MonoBehaviour
{
    public static MainGameState gameState;

    public List<Planet> planets;
    public int gameTime = 0;
    public UnityEvent gameStateUpdateEvent = new UnityEvent();
    public bool isTimerRunning = false;

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

    public void addListenerGameStateUpdateEvent(UnityAction call)
    {
        gameStateUpdateEvent.AddListener(call);
    }

    public void gameUpdateNotification()
    {
        gameStateUpdateEvent.Invoke();
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
    }

    public void startGameTimer()
    {

    }

    public void stopGameTimer()
    {

    }
}
