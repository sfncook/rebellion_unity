using UnityEngine;
using UnityEngine.UI;

public class PlanetStar : MonoBehaviour
{
    private Planet planet;

    private void Start()
    {
        MainGameState.gameState.filterChangeEvent.AddListener(onFilterChange);
    }

    void onFilterChange(FilterType filterType)
    {
        updatePlanetIcon();
    }

    public void setPlanet(Planet planet)
    {
        this.planet = planet;
        updatePlanetIcon();
    }

    private void updatePlanetIcon()
    {
        Color starColor = new Color(0.39f, 0.82f, 1f);
        Color color = Color.white;
        float scale = 1.0f;

        if (planet.isDiscovered)
        {
            switch (MainGameState.gameState.selectedFilterType)
            {
                case FilterType.PlanetsLoyalty:
                    color = planet.getTeam().getColorForTeam();
                    if (planet.getTeam() == Team.TeamA) { scale = getScale(0.5f, 1.0f, planet.loyalty); }
                    else { scale = getScale(0f, 0.5f, (0.5f - planet.loyalty)); }
                    break;
                case FilterType.PlanetsByName:
                    break;
                case FilterType.FactoriesAvailable:
                    if (planet.getTeam() == MainGameState.gameState.myTeam)
                    {
                        int manyAvailableFactories = 0;
                        foreach (Factory factory in planet.factories)
                        {
                            if (!factory.isBuilding)
                            {
                                manyAvailableFactories++;
                            }
                        }
                        scale = getScale(0, 2, manyAvailableFactories);
                        color = manyAvailableFactories > 0 ? starColor : Color.white;
                    }
                    break;
                case FilterType.CtorYards:
                    if (planet.getTeam() == MainGameState.gameState.myTeam)
                    {
                        int manyAvailableFactories = 0;
                        foreach (Factory factory in planet.factories)
                        {
                            if (!factory.isBuilding && factory.type == FactoryType.ctorYard)
                            {
                                manyAvailableFactories++;
                            }
                        }
                        scale = getScale(0, 2, manyAvailableFactories);
                        color = manyAvailableFactories > 0 ? starColor : Color.white;
                    }
                    break;
                case FilterType.TrainingFacs:
                    if (planet.getTeam() == MainGameState.gameState.myTeam)
                    {
                        int manyAvailableFactories = 0;
                        foreach (Factory factory in planet.factories)
                        {
                            if (!factory.isBuilding && factory.type == FactoryType.trainingFac)
                            {
                                manyAvailableFactories++;
                            }
                        }
                        scale = getScale(0, 2, manyAvailableFactories);
                        color = manyAvailableFactories > 0 ? starColor : Color.white;
                    }
                    break;
                case FilterType.ShipYards:
                    if (planet.getTeam() == MainGameState.gameState.myTeam)
                    {
                        int manyAvailableFactories = 0;
                        foreach (Factory factory in planet.factories)
                        {
                            if (!factory.isBuilding && factory.type == FactoryType.shipYard)
                            {
                                manyAvailableFactories++;
                            }
                        }
                        scale = getScale(0, 2, manyAvailableFactories);
                        color = manyAvailableFactories > 0 ? starColor : Color.white;
                    }
                    break;
                case FilterType.PersonnelAvailable:
                    int manyPersonnel = 0;
                    foreach (Ship ship in planet.shipsInOrbit)
                    {
                        if (ship.team == MainGameState.gameState.myTeam)
                        {
                            manyPersonnel += ship.personnelsOnBoard.Count;
                        }
                    }
                    foreach (Personnel personnel in planet.personnelsOnSurface)
                    {
                        if (personnel.team == MainGameState.gameState.myTeam)
                        {
                            manyPersonnel++;
                        }
                    }
                    scale = getScale(0, 5, manyPersonnel);
                    color = manyPersonnel > 0 ? starColor : Color.white;
                    break;
                case FilterType.Ships:
                    int manyShips = 0;
                    foreach (Ship ship in planet.shipsInOrbit)
                    {
                        if (ship.team == MainGameState.gameState.myTeam)
                        {
                            manyShips++;
                        }
                    }
                    scale = getScale(0, 3, manyShips);
                    color = manyShips > 0 ? starColor : Color.white;
                    break;
            }
        }

        transform.localScale = new Vector3(scale, scale, 1f);
        gameObject.GetComponent<Image>().color = color;
    }

    private float getScale(float min, float max, float val)
    {
        float newMax = max - min;
        float newVal = val - min;
        float scaleVal = ((newVal / newMax) * 2) + 0.5f;
        float scaleVal2 = Mathf.Min(Mathf.Max(scaleVal, 0.5f), 2.5f);
        return scaleVal2;
    }
}
