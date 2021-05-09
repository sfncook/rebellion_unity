using System.Collections.Generic;

public class Factory : AbstractUnit
{
    public bool isBuilding = false;
    public int buildingDaysRemaining = 0;
    public Planet planetDestination;
    public List<AbstractType> typesAvailableToBuild = new List<AbstractType>();
    public AbstractType buildingType;

    public Factory(FactoryType factoryType) : base(factoryType)
    {
    }
}
