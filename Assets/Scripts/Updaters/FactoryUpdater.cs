using UnityEngine;
using System.Collections.Generic;

public class FactoryUpdater : MonoBehaviour
{
    public static void updateBuilds(Planet planetSrc, Factory factory)
    {
        if(factory.isBuilding && MainGameState.gameState.gameTime >= factory.buildingDoneDay)
        {
            Planet planetDest = factory.planetDestination;
            factory.isBuilding = false;

            bool needsDelivery = !planetSrc.Equals(planetDest);
            int dayArrival = 0;
            if(needsDelivery)
            {
                dayArrival = MainGameState.arrivalDay(planetSrc, planetDest);
            }

            Debug.Log("planetSrc:"+planetSrc.name+" planetDst:"+planetDest.name+" needsDelivery:"+needsDelivery+" dayArrival:"+dayArrival);

            if (
                factory.buildingType.Equals(DefenseType.orbitalBattery) ||
                factory.buildingType.Equals(DefenseType.planetaryShield)
            )
            {
                Defense def = new Defense((DefenseType)factory.buildingType);
                if (needsDelivery)
                {
                    planetDest.defensesInTransit.Add(def);
                    def.dayArrival = dayArrival;
                } else
                {
                    planetDest.defenses.Add(def);
                }
            }

            else if (
                factory.buildingType.Equals(FactoryType.ctorYard) ||
                factory.buildingType.Equals(FactoryType.shipYard) ||
                factory.buildingType.Equals(FactoryType.trainingFac)
            )
            {
                Factory fct = new Factory((FactoryType)factory.buildingType);
                if (needsDelivery)
                {
                    planetDest.factoriesInTransit.Add(fct);
                    fct.dayArrival = dayArrival;
                }
                else
                {
                    planetDest.factoriesInTransit.Add(fct);
                }
            }


            else if (
                factory.buildingType.Equals(PersonnelType.Diplomat) ||
                factory.buildingType.Equals(PersonnelType.Soldiers) ||
                factory.buildingType.Equals(PersonnelType.Spy)
            )
            {
                planetSrc.personnelsOnSurface.Add(new Personnel((PersonnelType)factory.buildingType, planetSrc.getTeam()));
            }

            else if (
                factory.buildingType.Equals(ShipType.Bireme) ||
                factory.buildingType.Equals(ShipType.Trireme) ||
                factory.buildingType.Equals(ShipType.Quadreme) ||
                factory.buildingType.Equals(ShipType.Quintreme)
            )
            {
                Ship ship = new Ship((ShipType)factory.buildingType, planetSrc.getTeam());
                if (needsDelivery)
                {
                    planetDest.shipsInTransit.Add(ship);
                    ship.dayArrival = dayArrival;
                }
                else
                {
                    planetDest.shipsInTransit.Add(ship);
                }
            }
        }
    }
}
