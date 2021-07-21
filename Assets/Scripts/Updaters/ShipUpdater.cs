using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class ShipUpdater : MonoBehaviour
{
    public static void performAttackActions(Planet planet, Ship ship)
    {
        List<AbstractUnit> potentialTargets = new List<AbstractUnit>();

        List<Ship> enemyShipsInOrbit = planet.shipsInOrbit.Where(s => !s.team.Equals(ship.team)).ToList();
        potentialTargets.AddRange(enemyShipsInOrbit);

        bool hasShield = planet.defenses.Any(d => d.type.Equals(DefenseType.planetaryShield));
        if (!hasShield)
        {
            //Debug.Log("has NO shield: planet:"+planet.name+" loyalty:"+planet.loyalty+" team:"+ planet.getTeam());
            if (!planet.getTeam().Equals(ship.team))
            {
                potentialTargets.AddRange(planet.defenses);
                potentialTargets.AddRange(planet.factories);
            }

            List<Personnel> enemyPersonnelOnSurface = planet.personnelsOnSurface.Where(p => !p.team.Equals(ship.team)).ToList();
            potentialTargets.AddRange(enemyPersonnelOnSurface);
        }

        if(potentialTargets.Count>0)
        {
            string unitName = "";
            bool destroyed = false;
            int targetIndex = UnityEngine.Random.Range(0, potentialTargets.Count);
            //Debug.Log("ShipUpdater targetIndex:" + targetIndex + " out of potentialTargets.Count:" + potentialTargets.Count);
            AbstractUnit targetUnit = potentialTargets[targetIndex];

            if (targetUnit is Personnel)
            {
                Personnel targetUnitPersonnel = (Personnel)targetUnit;
                if (targetUnitPersonnel.type == PersonnelType.Soldiers)
                {
                    targetUnitPersonnel.manyPeopleDead = UnityEngine.Random.Range(1, targetUnitPersonnel.manyPeople);
                    unitName = "soldiers";
                }
                else
                {
                    targetUnitPersonnel.killed = true;
                    if (targetUnitPersonnel.isHero())
                    {
                        unitName = targetUnitPersonnel.hero.moniker;
                    }
                    else
                    {
                        unitName = targetUnitPersonnel.type.name;
                    }
                }
            }
            else if (targetUnit is Ship)
            {
                Ship targetShip = (Ship)targetUnit;
                float damage = UnityEngine.Random.Range(1f, targetShip.health);
                targetShip.health -= damage;
                //Debug.Log("Ship damage:" + damage + " new health:" + targetShip.health);
                unitName = ship.type.name;
                if (targetShip.health <= 0)
                {
                    destroyed = true;
                }
            }
            else if (targetUnit is Factory)
            {
                Factory targetFactory = (Factory)targetUnit;
                targetFactory.destroyed = true;
                destroyed = true;
                unitName = targetFactory.type.name;
            }
            else if (targetUnit is Defense)
            {
                Defense targetDefense = (Defense)targetUnit;
                int damage = UnityEngine.Random.Range(1, targetDefense.health);
                targetDefense.health -= damage;
                Debug.Log("Defense damage:" + damage + " new health:" + targetDefense.health);
                if (targetDefense.health <= 0)
                {
                    destroyed = true;
                }
                unitName = targetDefense.type.name;
            }

            Debug.Log(MainGameState.gameState.gameTime + " " + (destroyed ? "Destroyed" : "Damaged") + " " + unitName + " on planet:" + planet.name);
        }
    }// performAttackActions
}
