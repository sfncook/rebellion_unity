using UnityEngine;

public class FactoryUpdater : MonoBehaviour
{
    public static void updateBuilds(Factory factory)
    {
        if(factory.isBuilding && MainGameState.gameState.gameTime == factory.buildingDoneDay)
        {
            factory.isBuilding = false;
            if(factory.buildingType.Equals(DefenseType.orbitalBattery))
            {
            }
            else if (factory.buildingType.Equals(DefenseType.planetaryShield))
            {
            }
            else if (factory.buildingType.Equals(FactoryType.ctorYard))
            {
            }
            else if (factory.buildingType.Equals(FactoryType.shipYard))
            {
            }
            else if (factory.buildingType.Equals(FactoryType.trainingFac))
            {
            }
            else if (factory.buildingType.Equals(PersonnelType.Diplomat))
            {
            }
            else if (factory.buildingType.Equals(PersonnelType.Soldiers))
            {
            }
            else if (factory.buildingType.Equals(PersonnelType.Spy))
            {
            }
            else if (factory.buildingType.Equals(ShipType.Bireme))
            {
            }
            else if (factory.buildingType.Equals(ShipType.Trireme))
            {
            }
            else if (factory.buildingType.Equals(ShipType.Quadreme))
            {
            }
            else if (factory.buildingType.Equals(ShipType.Quintreme))
            {
            }
        }
    }
}
