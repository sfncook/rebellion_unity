using System;
using System.Collections.Generic;
using UnityEngine;

public class AllStoryLineUpdater
{
    public void init()
    {
        MainGameState.gameState.addListenerStoryLineUpdateEvent(onStoryLineUpdateEvent);
    }

    private void onStoryLineUpdateEvent()
    {
        // Initial storyline
        if(MainGameState.gameState.gameTime == 5)
        {
            Personnel initialHero = new Personnel(
                PersonnelType.Hero,
                Team.TeamA,
                hero: MainGameState.gameState.galaxy.heros[0],
                visibility:10,
                recruiting:80,
                diplomacy:10,
                espionage:10
            );
            MainGameState.gameState.homePlanet.personnelsOnSurface.Add(initialHero);
            string reportTitle = "Report from " + initialHero.hero.moniker;
            List<string> contentPages = new List<string>();
            contentPages.Add("Hello?  Is this thing working?  Ah, yes… You hear me.  For now I am safe enough.  My crypto-key indicator says that I am very unlikely to be detected.  But by simply talking with you it has been reduced slightly and I am more likely to be caught.  Or maybe even worse…");
            contentPages.Add("The Faction is becoming increasingly authoritarian.  They were able to infiltrate our collective.  Everyone I knew is missing.  Most have probably been killed.  But this was an act of desperation, can’t you see that?!  The loyalty on this planet is only slightly still in their favor, but the emotional indicator is no longer \"pacified\", but has only recently increased to a state of \"uprising\".");
            contentPages.Add("But all is not lost!  I evaded their capture and retained my crypto-siganture.  Intact and untraceable.  But I must continue to reach out to others who feel the way I do!  Who hate the damned Faction and their brutality!  And with each new contact, my crypto-signature becomes a little more visible, more traceable.");
            contentPages.Add("What should I do?  Please!  Tell me what to do!  I am lost, alone, and afraid for my life.  Perhaps I should try to recruit more people to the cause?  You can assign me a recruiting mission and I will seek out new people.  The more people we recruit, the more we can fan the flames of insurrection and sway the loyalty of the planet in our favor!");
            contentPages.Add("Who knows?  Perhaps in time we will have the majority of the planet’s loyalty.  Then we will control the factories and infrastructure.  Will be able to begin to build our own sanctuary here on this planet.  A place that we control and maybe, just maybe, we will be able to keep the damned Faction at bay.");
            contentPages.Add("(To assign "+ initialHero.hero.moniker + " a new recruiting mission click on their picture.)\n\n(Personnel can only be assigned to the planet where they are currently located.)");

            Report report = new StoryLineReport(reportTitle, initialHero, contentPages);
            MainGameState.gameState.reportsUnAcked.Add(report);
        }
    }
    
}
