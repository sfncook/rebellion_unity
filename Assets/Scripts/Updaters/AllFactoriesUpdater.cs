using UnityEngine;

public class AllFactoriesUpdater
{
    private MainGameState gameState;

    public AllFactoriesUpdater()
    {
        gameState = MainGameState.gameState;
        gameState.addPreDayPrepEvent(onPreDayPrepEvent);
    }

    public void onPreDayPrepEvent()
    {
        foreach (Planet planet in gameState.planets)
        {
            foreach (Factory factory in planet.factories)
            {
                FactoryUpdater.updateBuilds(factory);
            }
        }
    }

}
