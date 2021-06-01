using System.Collections.Generic;

public class FactoryType : AbstractType
{
    public static FactoryType ctorYard = new FactoryType("Construction Yard");
    public static FactoryType shipYard = new FactoryType("Ship Yard");
    public static FactoryType trainingFac = new FactoryType("Training Facility");

    public readonly List<AbstractType> typesAvailableToBuild = new List<AbstractType>();

    static FactoryType()
    {
        AbstractType[] ctorYardBuildTypes = { FactoryType.ctorYard, FactoryType.shipYard, FactoryType.trainingFac };
        AbstractType[] shipYardBuildTypes = { ShipType.Bireme, ShipType.Trireme, ShipType.Quadreme, ShipType.Quintreme };
        AbstractType[] trainingFacBuildTypes = { PersonnelType.Diplomat, PersonnelType.Soldiers, PersonnelType.Spy };
        ctorYard.typesAvailableToBuild.AddRange(ctorYardBuildTypes);
        shipYard.typesAvailableToBuild.AddRange(shipYardBuildTypes);
        trainingFac.typesAvailableToBuild.AddRange(trainingFacBuildTypes);
    }

    public FactoryType(string name) : base(name, TypeCategory.Factory)
    {
    }
}
