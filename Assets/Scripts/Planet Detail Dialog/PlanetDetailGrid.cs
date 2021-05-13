using UnityEngine;
using UnityEngine.UI;

public class PlanetDetailGrid : MonoBehaviour
{
    public GameObject defaultPrefab;
    public GameObject shipListItemPrefab;
    public int manyToCreate;

    private MainGameState gameState;

    [HideInInspector]
    public TabType selectedTab = TabType.Personnel;

    private Planet selectedPlanet;

    void Start()
    {
        gameState = MainGameState.gameState;
        selectedPlanet = gameState.planetForDetail;
        updateGrid();
    }

    public void setSelectedTab(TabType selectedTab)
    {
        this.selectedTab = selectedTab;
        updateGrid();
    }

    private void updateGrid()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject newObj;
        switch (selectedTab)
        {
            case TabType.Defense:
            case TabType.Factory:
            case TabType.Personnel:
                for (int i = 0; i < manyToCreate; i++)
                {
                    newObj = (GameObject)Instantiate(defaultPrefab, transform);
                    newObj.GetComponent<Image>().color = Random.ColorHSV();
                }
                break;
            case TabType.Ship:
                selectedPlanet.shipsInOrbit.Sort((a, b) => a.type.name.CompareTo(b.type.name));
                foreach (var ship in selectedPlanet.shipsInOrbit)
                {
                    newObj = (GameObject)Instantiate(shipListItemPrefab, transform);
                    ShipListItem shipListItem = newObj.GetComponent<ShipListItem>();
                    shipListItem.setShip(ship);
                }
                break;
        }
    }
}
