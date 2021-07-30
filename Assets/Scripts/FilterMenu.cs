using UnityEngine;
using System.Collections.Generic;

public class FilterMenu : MonoBehaviour
{
    public FilterMenuStack filterMenuStack;

    public void onClickPlanet()
    {
        filterMenuStack.setFilterTypes(new List<FilterType>() {
            FilterType.PlanetsLoyalty,
            FilterType.PlanetsByName
        });
        filterMenuStack.show();
    }

    public void onClickFactory()
    {
        filterMenuStack.setFilterTypes(new List<FilterType>() {
            FilterType.FactoriesAvailable,
            FilterType.CtorYards,
            FilterType.TrainingFacs,
            FilterType.ShipYards
        });
        filterMenuStack.show();
    }

    public void onClickPersonnel()
    {
        filterMenuStack.setFilterTypes(new List<FilterType>() {
            FilterType.PersonnelAvailable,
            FilterType.Soldiers,
            FilterType.Heros,
            FilterType.Spies,
            FilterType.Diplomats
        });
        filterMenuStack.show();
    }

    public void onClickShip()
    {
        filterMenuStack.setFilterTypes(new List<FilterType>() {
            FilterType.Ships
        });
        filterMenuStack.show();
    }
}
