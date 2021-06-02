public class Factory : AbstractUnit
{
    public bool isBuilding = false;
    public int buildingDoneDay = 0;
    public Planet planetDestination;
    public AbstractType buildingType;

    public Factory(FactoryType factoryType) : base(factoryType)
    {
    }
}
