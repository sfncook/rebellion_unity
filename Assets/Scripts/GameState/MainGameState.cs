using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MainGameState : MonoBehaviour
{
    public static MainGameState mainGameState;

    public List<StarSector> starSectors;
    public int gameTime = 0;

    void Awake()
    {
        if(mainGameState == null) {
            DontDestroyOnLoad(gameObject);
            mainGameState = this;
        }
        else if(mainGameState != this)
        {
            Destroy(gameObject);
        }
    }

    public StarSector getStarSectorByName(string sectorName)
    {
        return (from sec in mainGameState.starSectors where sec.name == sectorName select sec).SingleOrDefault();
    }

    public Planet getPlanetByName(string planetName)
    {
        foreach (StarSector sector in mainGameState.starSectors)
        {
            foreach (Planet planet in sector.planets)
            {
                if (planet.name.Equals(planetName))
                {
                    return planet;
                }
            }
        }
        return null;
    }

}
