using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FactoryBuildDialog2 : MonoBehaviour
{
    public HeaderControls headerControls;
    public Transform grid;
    public GameObject catalogListItemPrefab;
    public Button buildButton;

    private Factory factory;
    private CatalogListItem selectedCatalogListItem;

    void Start()
    {
        factory = MainGameState.gameState.factoryForDetail;
        headerControls.setHeaderTitle(factory.type.name);
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
        MainGameState.gameState.factoryForDetail = null;
        SceneManager.LoadScene("Planet Detail 2");
    }

    public void onClickBuild()
    {
        AbstractType type = selectedCatalogListItem.getType();
        factory.isBuilding = true;
        factory.buildingType = type;
        factory.buildingDoneDay = MainGameState.gameState.gameTime + type.daysToBuild;
        MainGameState.gameState.factoryForDetail = null;
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

        buildButton.gameObject.SetActive(true);
    }
}
