using System.Collections.Generic;

public class FactoryType : AbstractType
{
    public static FactoryType ctorYard = new FactoryType("Construction Yard", true);
    public static FactoryType shipYard = new FactoryType("Ship Yard", false);
    public static FactoryType trainingFac = new FactoryType("Training Facility", false);

    public readonly List<AbstractType> typesAvailableToBuild = new List<AbstractType>();
    public readonly bool buildTypesRequireEnergy = false;

    static FactoryType()
    {
        AbstractType[] ctorYardBuildTypes = { FactoryType.ctorYard, FactoryType.shipYard, FactoryType.trainingFac };
        AbstractType[] shipYardBuildTypes = { ShipType.Bireme, ShipType.Trireme, ShipType.Quadreme, ShipType.Quintreme };
        AbstractType[] trainingFacBuildTypes = { PersonnelType.Diplomat, PersonnelType.Soldiers, PersonnelType.Spy };
        ctorYard.typesAvailableToBuild.AddRange(ctorYardBuildTypes);
        shipYard.typesAvailableToBuild.AddRange(shipYardBuildTypes);
        trainingFac.typesAvailableToBuild.AddRange(trainingFacBuildTypes);
    }

    public FactoryType(string name, bool buildTypesRequireEnergy) : base(name, TypeCategory.Factory)
    {
        this.buildTypesRequireEnergy = buildTypesRequireEnergy;
    }
}
