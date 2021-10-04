using UnityEngine;
using System.Collections.Generic;

public class AllPersonnelUpdater
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

    // 3. - Battles takes place
    //    - pieces take damage
    public void onAgentActionEvent()
    {
        foreach (StarSector sector in gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                foreach (Personnel personnel in planet.personnelsOnSurface)
                {
                    PersonnelUpdater.performAttackActions(planet, personnel);
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
                List<Personnel> personnelToDelete = new List<Personnel>();
                foreach (Personnel personnel in planet.personnelsOnSurface)
                {
                    if(personnel.killed)
                    {
                        personnelToDelete.Add(personnel);
                    }
                    if (personnel.manyPeopleDead > 0)
                    {
                        personnel.manyPeople -= personnel.manyPeopleDead;
                        personnel.manyPeopleDead = 0;
                    }
                    if (personnel.manyPeople <= 0)
                    {
                        personnelToDelete.Add(personnel);
                    }
                }
                foreach (Personnel personnelToDelete_ in personnelToDelete)
                {
                    //Debug.Log(planet.name + " Personnel destroyed:" + personnelToDelete_.type.name + " team:" + personnelToDelete_.team);
                    planet.personnelsOnSurface.Remove(personnelToDelete_);
                    MainGameState.gameState.addGameEvent("Personnel killed:" + personnelToDelete_.type.ToString() + " Plt:" + planet.name);
                }
            }
        }
    }
}
