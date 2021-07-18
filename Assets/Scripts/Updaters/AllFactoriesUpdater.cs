using System.Collections.Generic;

public class AllFactoriesUpdater
{
    private MainGameState gameState;

    public void init()
    {
        gameState = MainGameState.gameState;
        gameState.addPreDayPrepEvent(onPreDayPrepEvent);
        gameState.addListenerPostCleanupEvent(onPostCleanupEvent);
    }

    public void onPreDayPrepEvent()
    {
        foreach (StarSector sector in gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                foreach (Factory factory in planet.factories)
                {
                    FactoryUpdater.updateBuilds(planet, factory);
                }
            }
        }
    }

    public void onPostCleanupEvent()
    {
        foreach (StarSector sector in gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                List<Factory> factoriesToDelete = new List<Factory>();
                foreach (Factory factory in planet.factories)
                {
                    if (factory.destroyed)
                    {
                        factoriesToDelete.Add(factory);
                    }
                }
                foreach (Factory factoryToDelete in factoriesToDelete)
                {
                    planet.factories.Remove(factoryToDelete);
                }
            }
        }
    }

}
