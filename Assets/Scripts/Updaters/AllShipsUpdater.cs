using UnityEngine;
using System.Collections.Generic;

public class AllShipsUpdater
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
        //Debug.Log("onAgentActionEvent");
        foreach (StarSector sector in gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                foreach (Ship ship in planet.shipsInOrbit)
                {
                    ShipUpdater.performAttackActions(planet, ship);
                }
            }
        }
    }

    public void onPostCleanupEvent()
    {
        //Debug.Log("onPostCleanupEvent");
        foreach (StarSector sector in gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                List<Ship> shipsToDelete = new List<Ship>();
                foreach (Ship ship in planet.shipsInOrbit)
                {
                    //Debug.Log(planet.name+" "+ship.team+" "+ship.type.name+" health:"+ship.health);
                    if (ship.health <= 0)
                    {
                        shipsToDelete.Add(ship);
                    }
                }
                foreach (Ship shipToDelete in shipsToDelete)
                {
                    Debug.Log(planet.name+" Ship destroyed:" + shipToDelete.type.name + " team:" + shipToDelete.team);
                    planet.shipsInOrbit.Remove(shipToDelete);
                }
            }
        }
    }
}
