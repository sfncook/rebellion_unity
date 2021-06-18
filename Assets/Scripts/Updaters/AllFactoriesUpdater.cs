public class AllFactoriesUpdater
{
    private MainGameState gameState;

    public void init()
    {
        gameState = MainGameState.gameState;
        gameState.addPreDayPrepEvent(onPreDayPrepEvent);
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

}
