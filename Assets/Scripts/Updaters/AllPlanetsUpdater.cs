using System.Collections.Generic;
using UnityEngine;

public class AllPlanetsUpdater
{

    public void init()
    {
        MainGameState.gameState.addListenerUiUpdateEvent(onUiUpdateEvent);
        MainGameState.gameState.addListenerPostCleanupEvent(onPostCleanupEvent);
    }

    void onUiUpdateEvent()
    {
    }

    public void onPostCleanupEvent()
    {
        foreach (StarSector sector in MainGameState.gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                // Ships
                List<Ship> shipsArrival = new List<Ship>();
                foreach (Ship ship in planet.shipsInTransit)
                {
                    if (ship.dayArrival <= MainGameState.gameState.gameTime)
                    {
                        ship.dayArrival = 0;
                        shipsArrival.Add(ship);
                    }
                }
                foreach(Ship ship in shipsArrival)
                {
                    planet.shipsInTransit.Remove(ship);
                    planet.shipsInOrbit.Add(ship);
                    planet.setDiscoveredByTeam(ship.team);
                    MainGameState.gameState.addGameEvent("Ship arrived:" + ship.type.ToString() + " Plt:" + planet.name);
                }
                planet.shipsInOrbit.AddRange(planet.shipsToDeploy);
                planet.shipsToDeploy.Clear();


                // Personnel
                List<Personnel> peopleArrival = new List<Personnel>();
                foreach (Personnel personnel in planet.personnelsInTransit)
                {
                    if (personnel.dayArrival <= MainGameState.gameState.gameTime)
                    {
                        personnel.dayArrival = 0;
                        peopleArrival.Add(personnel);
                        MainGameState.gameState.addGameEvent("Personnel arrived:" + personnel.type.ToString() + " Plt:" + planet.name);
                    }
                }
                foreach (Personnel personnel in peopleArrival)
                {
                    planet.personnelsInTransit.Remove(personnel);
                    planet.personnelsOnSurface.Add(personnel);
                    planet.setDiscoveredByTeam(personnel.team);
                }
                planet.personnelsOnSurface.AddRange(planet.personnelsToDeploy);
                planet.personnelsToDeploy.Clear();


                // Factories
                List<Factory> factoriesArrival = new List<Factory>();
                foreach (Factory factory in planet.factoriesInTransit)
                {
                    Debug.Log("factory.dayArrival:"+ factory.dayArrival);
                    if (factory.dayArrival <= MainGameState.gameState.gameTime)
                    {
                        factory.dayArrival = 0;
                        factoriesArrival.Add(factory);
                        MainGameState.gameState.addGameEvent("Factory arrived:" + factory.type.ToString() + " Plt:" + planet.name);
                    }
                }
                foreach (Factory factory in factoriesArrival)
                {
                    planet.factoriesInTransit.Remove(factory);
                    planet.factories.Add(factory);
                }
                planet.factories.AddRange(planet.factoriesToDeploy);
                planet.factoriesToDeploy.Clear();


                // Defense
                List<Defense> defensesArrival = new List<Defense>();
                foreach (Defense defense in planet.defensesInTransit)
                {
                    if (defense.dayArrival <= MainGameState.gameState.gameTime)
                    {
                        defense.dayArrival = 0;
                        defensesArrival.Add(defense);
                    }
                }
                foreach (Defense defense in defensesArrival)
                {
                    planet.defensesInTransit.Remove(defense);
                    planet.defenses.Add(defense);
                    MainGameState.gameState.addGameEvent("Defense-structure arrived:" + defense.type.ToString() + " Plt:" + planet.name);
                }
                planet.defenses.AddRange(planet.defensesToDeploy);
                planet.defensesToDeploy.Clear();
            }
        }
    }
}
