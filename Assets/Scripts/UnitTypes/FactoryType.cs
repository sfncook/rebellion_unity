using System.Collections.Generic;

public class FactoryType : AbstractType
{
    public static FactoryType ctorYard = new FactoryType("Construction Yard", true, true);
    public static FactoryType shipYard = new FactoryType("Ship Yard", false, true);
    public static FactoryType trainingFac = new FactoryType("Training Facility", false, false);

    public readonly List<AbstractType> typesAvailableToBuild = new List<AbstractType>();
    public readonly bool buildTypesRequireEnergy = false;
    public readonly bool buildTypesAreDeliverable = false;

    static FactoryType()
    {
        AbstractType[] ctorYardBuildTypes = { FactoryType.ctorYard, FactoryType.shipYard, FactoryType.trainingFac };
        AbstractType[] shipYardBuildTypes = { ShipType.Bireme, ShipType.Trireme, ShipType.Quadreme, ShipType.Quintreme };
        AbstractType[] trainingFacBuildTypes = { PersonnelType.Diplomat, PersonnelType.Soldiers, PersonnelType.Spy };
        ctorYard.typesAvailableToBuild.AddRange(ctorYardBuildTypes);
        shipYard.typesAvailableToBuild.AddRange(shipYardBuildTypes);
        trainingFac.typesAvailableToBuild.AddRange(trainingFacBuildTypes);
    }

    public FactoryType(string name, bool buildTypesRequireEnergy, bool buildTypesAreDeliverable) : base(name, TypeCategory.Factory)
    {
        this.buildTypesRequireEnergy = buildTypesRequireEnergy;
        this.buildTypesAreDeliverable = buildTypesAreDeliverable;
    }
}
