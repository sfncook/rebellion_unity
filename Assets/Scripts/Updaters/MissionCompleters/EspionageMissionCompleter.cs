using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EspionageMissionCompleter : MissionCompleter
{
    public override MissionReport completeMission(StarSector sector, Planet planet, Personnel reporter)
    {
        bool missionSuccess = didMissionSucceed(reporter, reporter.diplomacy);
        AbstractUnit targetUnit = null;
        bool destroyed = false;
        if (missionSuccess)
        {
            Team planetTeam = planet.getTeam();
            Team myTeam = MainGameState.gameState.myTeam;
            List<AbstractUnit> potentialTargets = new List<AbstractUnit>();

            List<Personnel> enemyPersonnelOnSurface = planet.personnelsOnSurface.Where(p => !p.team.Equals(myTeam)).ToList();
            potentialTargets.AddRange(enemyPersonnelOnSurface);

            List<Ship> enemyShipsInOrbit = planet.shipsInOrbit.Where(s => !s.team.Equals(myTeam)).ToList();
            potentialTargets.AddRange(enemyShipsInOrbit);

            if (!planetTeam.Equals(myTeam))
            {
                potentialTargets.AddRange(planet.defenses);
                potentialTargets.AddRange(planet.factories);
            }

            targetUnit = potentialTargets[Random.Range(0, potentialTargets.Count)];

            if (targetUnit is Personnel)
            {
                Personnel targetUnitPersonnel = (Personnel)targetUnit;
                if (targetUnitPersonnel.type == PersonnelType.Soldiers)
                {
                    targetUnitPersonnel.manyPeopleDead = Random.Range(1, targetUnitPersonnel.manyPeople);
                } else
                {
                    targetUnitPersonnel.killed = true;
                }
            }
            else if (targetUnit is Ship)
            {
                Ship targetShip = (Ship)targetUnit;
                targetShip.health -= Random.Range(1, targetShip.health);
                if(targetShip.health <= 0)
                {
                    destroyed = true;
                }
            }
            else if (targetUnit is Factory)
            {
                Factory targetFactory = (Factory)targetUnit;
                targetFactory.destroyed = true;
                destroyed = true;
            }
            else if (targetUnit is Defense)
            {
                Defense targetDefense = (Defense)targetUnit;
                targetDefense.health -= Random.Range(1, targetDefense.health);
                if (targetDefense.health <= 0)
                {
                    destroyed = true;
                }
            }
        }
        return new EspionageMissionReport(reporter, missionSuccess, MainGameState.gameState.gameTime, false, targetUnit);
    }
}
