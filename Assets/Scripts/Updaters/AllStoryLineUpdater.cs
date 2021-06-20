using System;
public class AllStoryLineUpdater
{
    public void init()
    {
        MainGameState.gameState.addListenerStoryLineUpdateEvent(onStoryLineUpdateEvent);
    }

    private void onStoryLineUpdateEvent()
    {
        // Initial storyline
        if(MainGameState.gameState.gameTime == 3)
        {
            Personnel reporter = MainGameState.gameState.initialHero;
            string reportTitle = "Report from " + reporter.hero.moniker;
            Report report = new StoryLineReport(reportTitle, MainGameState.gameState.initialHero);
            MainGameState.gameState.reportsUnAcked.Add(report);
        }
    }
    
}
