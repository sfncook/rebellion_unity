using UnityEngine;

public class FactoryUpdater : MonoBehaviour
{
    public static void updateBuilds(Planet planet, Factory factory)
    {
        if(factory.isBuilding && MainGameState.gameState.gameTime >= factory.buildingDoneDay)
        {
            Planet planetDest = factory.planetDestination;
            factory.isBuilding = false;
            if(factory.buildingType.Equals(DefenseType.orbitalBattery))
            {
                planetDest.defenses.Add(new Defense(DefenseType.orbitalBattery));
            }
            else if (factory.buildingType.Equals(DefenseType.planetaryShield))
            {
                planetDest.defenses.Add(new Defense(DefenseType.planetaryShield));
            }
            else if (factory.buildingType.Equals(FactoryType.ctorYard))
            {
                planetDest.factories.Add(new Factory(FactoryType.ctorYard));
            }
            else if (factory.buildingType.Equals(FactoryType.shipYard))
            {
                planetDest.factories.Add(new Factory(FactoryType.shipYard));
            }
            else if (factory.buildingType.Equals(FactoryType.trainingFac))
            {
                planetDest.factories.Add(new Factory(FactoryType.trainingFac));
            }
            else if (factory.buildingType.Equals(PersonnelType.Diplomat))
            {
                planetDest.personnelsOnSurface.Add(new Personnel(PersonnelType.Diplomat, planet.getTeam()));
            }
            else if (factory.buildingType.Equals(PersonnelType.Soldiers))
            {
                planetDest.personnelsOnSurface.Add(new Personnel(PersonnelType.Soldiers, planet.getTeam()));
            }
            else if (factory.buildingType.Equals(PersonnelType.Spy))
            {
                planetDest.personnelsOnSurface.Add(new Personnel(PersonnelType.Spy, planet.getTeam()));
            }
            else if (factory.buildingType.Equals(ShipType.Bireme))
            {
                planetDest.shipsInOrbit.Add(new Ship(ShipType.Bireme, planet.getTeam()));
            }
            else if (factory.buildingType.Equals(ShipType.Trireme))
            {
                planetDest.shipsInOrbit.Add(new Ship(ShipType.Trireme, planet.getTeam()));
            }
            else if (factory.buildingType.Equals(ShipType.Quadreme))
            {
                planetDest.shipsInOrbit.Add(new Ship(ShipType.Quadreme, planet.getTeam()));
            }
            else if (factory.buildingType.Equals(ShipType.Quintreme))
            {
                planetDest.shipsInOrbit.Add(new Ship(ShipType.Quintreme, planet.getTeam()));
            }
        }
    }
}
