using UnityEngine;
using System.Collections.Generic;
using System;

public class DefenseUpdater : MonoBehaviour
{
    public static void performAttackActions(Planet planet, Defense defense)
    {
        if(defense.type.Equals(DefenseType.orbitalBattery))
        {
            List<Ship> enemyShips = new List<Ship>();
            foreach (Ship otherShip in planet.shipsInOrbit)
            {
                if (!otherShip.team.Equals(MainGameState.gameState.myTeam))
                {
                    enemyShips.Add(otherShip);
                }
            }

            if (enemyShips.Count > 0)
            {
                Ship enemyShipToAttack = enemyShips[UnityEngine.Random.Range(0, enemyShips.Count)];
                ShipType enemyShipType = (ShipType)enemyShipToAttack.type;
                DefenseType defenseType = (DefenseType)defense.type;

                float attackValue = UnityEngine.Random.Range(0.0f, (float)defenseType.attackStrength);
                float defenseValue = UnityEngine.Random.Range(0.0f, (float)enemyShipType.defenseStrength);
                float damage = Math.Max(attackValue - defenseValue, 0);
                enemyShipToAttack.health -= damage;
            }
        }
    }// performAttackActions
}
