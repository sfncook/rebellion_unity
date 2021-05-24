using UnityEngine;
using System.Collections.Generic;

public class AllPersonnelUpdater : MonoBehaviour
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

    // 3. - Battles takes place
    //    - pieces take damage
    public void onAgentActionEvent()
    {
        foreach (Planet planet in gameState.planets)
        {
            foreach (Personnel personnel in planet.personnelsOnSurface)
            {
                PersonnelUpdater.performAttackActions(planet, personnel);
            }
        }
    }

    public void onPostCleanupEvent()
    {
        foreach (Planet planet in gameState.planets)
        {
            List<Personnel> personnelToDelete = new List<Personnel>();
            foreach (Personnel personnel in planet.personnelsOnSurface)
            {
                if (personnel.manyPeopleDead>0)
                {
                    personnel.manyPeople -= personnel.manyPeopleDead;
                    personnel.manyPeopleDead = 0;
                }
                if(personnel.manyPeople <= 0)
                {
                    personnelToDelete.Add(personnel);
                }
            }
            foreach(Personnel personnelToDelete_ in personnelToDelete)
            {
                Debug.Log("Personnel destroyed:"+ personnelToDelete_.type.name+" team:"+ personnelToDelete_.team);
                planet.personnelsOnSurface.Remove(personnelToDelete_);
            }
        }
    }
}
