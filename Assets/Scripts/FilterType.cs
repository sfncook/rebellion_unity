using System;
using System.Collections.Generic;

public enum FilterType
{
    PlanetsLoyalty,
    PlanetsByName,
    FactoriesAvailable,
    CtorYards,
    TrainingFacs,
    ShipYards,
    PersonnelAvailable,
    Soldiers,
    Heros,
    Spies,
    Diplomats,
    Ships
}

public static class FilterTypeHelper
{
    public static Dictionary<FilterType, String> filterLabels = new Dictionary<FilterType, String> {
        { FilterType.PlanetsLoyalty, "Planetary Loyalty"},
        { FilterType.PlanetsByName, "Find Planet By Name"},
        { FilterType.FactoriesAvailable, "Available Factories"},
        { FilterType.CtorYards, "Construction Yards"},
        { FilterType.TrainingFacs, "Training Facilities"},
        { FilterType.ShipYards, "Ship Yards"},
        { FilterType.PersonnelAvailable, "Available Personnel"},
        { FilterType.Soldiers, "Soldiers"},
        { FilterType.Heros, "Heros"},
        { FilterType.Spies, "Spies"},
        { FilterType.Diplomats, "Diplomats"},
        { FilterType.Ships, "Available Ships"}
    };
}
