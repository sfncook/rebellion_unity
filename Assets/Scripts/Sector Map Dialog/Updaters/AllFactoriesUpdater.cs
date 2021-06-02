using UnityEngine;

public class AllFactoriesUpdater : MonoBehaviour
{
    private MainGameState gameState;

    void Start()
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
