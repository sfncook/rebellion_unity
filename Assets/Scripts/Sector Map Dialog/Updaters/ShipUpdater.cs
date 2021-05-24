using UnityEngine;
using System.Collections.Generic;
using System;

public class ShipUpdater : MonoBehaviour
{
    public static void performAttackActions(Planet planet, Ship ship)
    {
        List<Ship> enemyShips = new List<Ship>();
        foreach(Ship otherShip in planet.shipsInOrbit)
        {
            if(!otherShip.team.Equals(ship.team))
            {
                enemyShips.Add(otherShip);
            }
        }

        if (enemyShips.Count>0)
        {
            Ship enemyShipToAttack = enemyShips[UnityEngine.Random.Range(0, enemyShips.Count)];
            ShipType enemyShipType = (ShipType) enemyShipToAttack.type;
            ShipType shipType = (ShipType)ship.type;
            for (var i=0; i<shipType.manyAttacksPerTurn; i++)
            {
                float attackValue = UnityEngine.Random.Range(0.0f, (float) shipType.attackStrength);
                float defenseValue = UnityEngine.Random.Range(0.0f, (float) enemyShipType.defenseStrength);
                float damage = Math.Max(attackValue - defenseValue, 0);
                enemyShipToAttack.health -= damage;
                //Debug.Log("Ship damage offense:"+ship.type.name+" defense:"+enemyShipToAttack.type.name+" health:"+enemyShipToAttack.health+" damage:"+damage+" enemyTeam:"+enemyShipToAttack.team);
            }
        }
    }// performAttackActions
}
