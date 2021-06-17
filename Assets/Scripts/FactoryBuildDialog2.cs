using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FactoryBuildDialog2 : MonoBehaviour
{
    public HeaderControls headerControls;
    public Transform grid;
    public GameObject catalogListItemPrefab;
    public Button buildButton;
    public SectorMap2 sectorMap;
    public GalaxyMap galaxyMap;
    public GameObject toGalaxyButton;
    public TextMeshProUGUI sectorNameText;

    private Factory factory;
    private CatalogListItem selectedCatalogListItem;

    void Start()
    {
        factory = MainGameState.gameState.factoryForDetail;
        bool buildTypesRequireEnergy = ((FactoryType)factory.type).buildTypesRequireEnergy;
        if (
             buildTypesRequireEnergy  && MainGameState.gameState.planetForDetail.getManyAvailableEnergies() > 0 ||
             !buildTypesRequireEnergy
            )
        {
            MainGameState.gameState.planetSelectedForDestination = MainGameState.gameState.planetForDetail;
        }
        headerControls.setHeaderTitle(factory.type.name);
        sectorNameText.text = MainGameState.gameState.sectorForDetail.name;
        sectorMap.setSector(MainGameState.gameState.sectorForDetail);
        updateGrid();
    }

    private void updateGrid()
    {
        clearGrid();

        GameObject newObj;
        FactoryType factoryType = (FactoryType)factory.type;
        foreach (AbstractType typeToBuild in factoryType.typesAvailableToBuild)
        {
            newObj = (GameObject)Instantiate(catalogListItemPrefab, grid);
            CatalogListItem catalogListItem = newObj.GetComponent<CatalogListItem>();
            catalogListItem.setType(typeToBuild);
            catalogListItem.setOnSelectItem(onSelectCatalogItem);
        }
    }

    private void clearGrid()
    {
        foreach (Transform child in grid)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void onClickBackButton()
    {
        MainGameState.gameState.planetSelectedForDestination = null;
        MainGameState.gameState.factoryForDetail = null;
        SceneManager.LoadScene("Planet Detail 2");
    }

    public void onClickBuild()
    {
        AbstractType type = selectedCatalogListItem.getType();
        factory.isBuilding = true;
        factory.buildingType = type;
        factory.buildingDoneDay = MainGameState.gameState.gameTime + type.daysToBuild;
        factory.planetDestination = MainGameState.gameState.planetSelectedForDestination;
        MainGameState.gameState.factoryForDetail = null;
        MainGameState.gameState.planetSelectedForDestination = null;
        SceneManager.LoadScene("Planet Detail 2");
    }

    private void onSelectCatalogItem(CatalogListItem selectedCatalogListItem)
    {
        this.selectedCatalogListItem = selectedCatalogListItem;

        CatalogListItem[] catalogListItems = grid.GetComponentsInChildren<CatalogListItem>();
        foreach (CatalogListItem catalogListItem in catalogListItems)
        {
            if (catalogListItem.Equals(this.selectedCatalogListItem))
            {
                catalogListItem.setSelected(true);
            }
            else
            {
                catalogListItem.setSelected(false);
            }
        }

        updateBuildButton();
    }

    public void onClickSector(StarSector sector)
    {
        sectorNameText.text = sector.name;
        sectorMap.setSector(sector);
        galaxyMap.gameObject.SetActive(false);
        sectorMap.gameObject.SetActive(true);
        toGalaxyButton.SetActive(true);
        sectorNameText.gameObject.SetActive(true);
    }

    public void onClickPlanet(Planet planet)
    {
        bool buildTypesRequireEnergy = ((FactoryType)factory.type).buildTypesRequireEnergy;
        if (
             buildTypesRequireEnergy && planet.getManyAvailableEnergies() > 0 ||
             !buildTypesRequireEnergy
            )
        {
            sectorMap.selectPlanet(planet);
            updateBuildButton();
        }
    }

    private void updateBuildButton()
    {
        if (selectedCatalogListItem != null && MainGameState.gameState.planetSelectedForDestination!=null)
        {
            buildButton.gameObject.SetActive(true);
        } else
        {
            buildButton.gameObject.SetActive(false);
        }
    }
}
