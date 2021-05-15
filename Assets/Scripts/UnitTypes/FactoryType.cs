using System.Collections.Generic;

public class FactoryType : AbstractType
{
    private static AbstractType[] ctorYardBuildTypes = {FactoryType.ctorYard, FactoryType.shipYard, FactoryType.trainingFac};
    private static AbstractType[] shipYardBuildTypes = {ShipType.Bireme, ShipType.Trireme, ShipType.Quadreme, ShipType.Quintreme};
    private static AbstractType[] trainingFacBuildTypes = {PersonnelType.Diplomat};

    public static FactoryType ctorYard = new FactoryType("Construction Yard", new List<AbstractType>(ctorYardBuildTypes));
    public static FactoryType shipYard = new FactoryType("Ship Yard", new List<AbstractType>(shipYardBuildTypes));
    public static FactoryType trainingFac = new FactoryType("Training Facility", new List<AbstractType>(trainingFacBuildTypes));

    public readonly List<AbstractType> typesAvailableToBuild = new List<AbstractType>();

    public FactoryType(string name, List<AbstractType> typesAvailableToBuild) : base(name)
    {
        typesAvailableToBuild = new List<AbstractType>(typesAvailableToBuild);
    }
}
