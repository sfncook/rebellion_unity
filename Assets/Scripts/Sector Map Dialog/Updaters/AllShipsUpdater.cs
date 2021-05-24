using UnityEngine;
using System.Collections.Generic;

public class AllShipsUpdater : MonoBehaviour
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
            foreach (Ship ship in planet.shipsInOrbit)
            {
                ShipUpdater.performAttackActions(planet, ship);
            }
        }
    }

    public void onPostCleanupEvent()
    {
        foreach (Planet planet in gameState.planets)
        {
            List<Ship> shipsToDelete = new List<Ship>();
            foreach (Ship ship in planet.shipsInOrbit)
            {
                if (ship.health<=0)
                {
                    shipsToDelete.Add(ship);
                }
            }
            foreach(Ship shipToDelete in shipsToDelete)
            {
                //Debug.Log("Ship destroyed:"+shipToDelete.type.name+" team:"+shipToDelete.team);
                planet.shipsInOrbit.Remove(shipToDelete);
            }
        }
    }
}
