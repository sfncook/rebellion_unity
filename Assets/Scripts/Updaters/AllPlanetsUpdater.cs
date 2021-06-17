using System.Collections.Generic;

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
                //TODO:
                //  - When planet loyalty changes cancel all factory builds
                // Ships
                List<Ship> shipsArrival = new List<Ship>();
                foreach (Ship ship in planet.shipsInTransit)
                {
                    if (ship.dayArrival >= MainGameState.gameState.gameTime)
                    {
                        ship.dayArrival = 0;
                        shipsArrival.Add(ship);
                    }
                }
                foreach(Ship ship in shipsArrival)
                {
                    planet.shipsInTransit.Remove(ship);
                    planet.shipsInOrbit.Add(ship);
                }


                // Personnel
                List<Personnel> peopleArrival = new List<Personnel>();
                foreach (Personnel personnel in planet.personnelsInTransit)
                {
                    if (personnel.dayArrival >= MainGameState.gameState.gameTime)
                    {
                        personnel.dayArrival = 0;
                        peopleArrival.Add(personnel);
                    }
                }
                foreach (Personnel personnel in peopleArrival)
                {
                    planet.personnelsInTransit.Remove(personnel);
                    planet.personnelsOnSurface.Add(personnel);
                }


                // Factories
                List<Factory> factoriesArrival = new List<Factory>();
                foreach (Factory factory in planet.factoriesInTransit)
                {
                    if (factory.dayArrival >= MainGameState.gameState.gameTime)
                    {
                        factory.dayArrival = 0;
                        factoriesArrival.Add(factory);
                    }
                }
                foreach (Factory factory in factoriesArrival)
                {
                    planet.factoriesInTransit.Remove(factory);
                    planet.factories.Add(factory);
                }


                // Defense
                List<Defense> defensesArrival = new List<Defense>();
                foreach (Defense defense in planet.defensesInTransit)
                {
                    if (defense.dayArrival >= MainGameState.gameState.gameTime)
                    {
                        defense.dayArrival = 0;
                        defensesArrival.Add(defense);
                    }
                }
                foreach (Defense defense in defensesArrival)
                {
                    planet.defensesInTransit.Remove(defense);
                    planet.defenses.Add(defense);
                }
            }
        }
    }
}
