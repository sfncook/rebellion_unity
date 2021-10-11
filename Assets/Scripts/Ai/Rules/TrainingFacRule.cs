using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainingFacRule : Rule
{
    public override void apply(Dictionary<AbstractUnit, UnitAction> unitActions)
    {
        Debug.Log("TrainingFacRule");
        foreach (StarSector sector in MainGameState.gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                if(planet.getTeam() == MainGameState.gameState.enemyTeam)
                {
                    List<Factory> availableTrainingFacs = planet.factories.Where(f => (f.type == FactoryType.trainingFac && !f.isBuilding)).ToList();
                    foreach(Factory factory in availableTrainingFacs)
                    {
                        if(personnelOnSurfaceForTeam(planet, MainGameState.gameState.enemyTeam) <= 50)
                        {
                            unitActions[factory] = UnitAction.trainPersonnel(planet, PersonnelType.Soldiers);
                        }
                    }
                }
            }
        }
    }
}
