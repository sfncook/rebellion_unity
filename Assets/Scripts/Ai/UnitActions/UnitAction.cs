public readonly struct UnitAction
{
    public enum UnitActionType
    {
        TrainPersonnel
    }

    public UnitAction(UnitActionType _unitActionType, Planet _targetPlanet, AbstractType _factoryBuildType)
    {
        targetPlanet = _targetPlanet;
        unitActionType = _unitActionType;
        factoryBuildType = _factoryBuildType;
    }

    public Planet targetPlanet { get; }
    public UnitActionType unitActionType { get; }
    public AbstractType factoryBuildType { get; }

    public static UnitAction trainPersonnel(Planet planet, PersonnelType personnelType) { return new UnitAction(UnitActionType.TrainPersonnel, planet, personnelType); }
}
