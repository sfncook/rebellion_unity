using UnityEngine;
using System.Collections.Generic;

public class AllDefenseUpdater : MonoBehaviour
{
    private MainGameState gameState;

    void Start()
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
        foreach (Planet planet in gameState.planets)
        {
            foreach (Defense defense in planet.defenses)
            {
                DefenseUpdater.performAttackActions(planet, defense);
            }
        }
    }

    public void onPostCleanupEvent()
    {
        foreach (Planet planet in gameState.planets)
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
