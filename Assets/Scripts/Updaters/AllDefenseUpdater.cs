using UnityEngine;
using System.Collections.Generic;

public class AllDefenseUpdater
{
    private MainGameState gameState;

    public void init()
    {
        gameState = MainGameState.gameState;
        gameState.addListenerAgentPlanEvent(onAgentPlanEvent);
        gameState.addListenerAgentActionEvent(onAgentActionEvent);
        gameState.addListenerPostCleanupEvent(onPostCleanupEvent);
    }

    public void onAgentPlanEvent()
    {

    }

    public void onAgentActionEvent()
    {
        foreach (StarSector sector in gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                foreach (Defense defense in planet.defenses)
                {
                    DefenseUpdater.performAttackActions(planet, defense);
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
                List<Defense> defensesToDelete = new List<Defense>();
                foreach (Defense defense in planet.defenses)
                {
                    if (defense.health <= 0)
                    {
                        defensesToDelete.Add(defense);
                    }
                }
                foreach (Defense defenseToDelete in defensesToDelete)
                {
                    planet.defenses.Remove(defenseToDelete);
                }
            }
        }
    }
}
