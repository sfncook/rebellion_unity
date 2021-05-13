using UnityEngine;
using UnityEngine.UI;

public class PlanetDetailGrid : MonoBehaviour
{
    public GameObject prefab;
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
        GameObject newObj;

        for(int i=0; i<manyToCreate; i++)
        {
            newObj = (GameObject)Instantiate(prefab, transform);
            newObj.GetComponent<Image>().color = Random.ColorHSV();
        }
    }
}
