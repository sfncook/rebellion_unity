using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class AssignFactoryBuildCommandEvent : UnityEvent<Factory, AbstractType>
{
}

public class FactoryDialog : MonoBehaviour
{
    public TextMeshProUGUI labelText;
    public Image factoryImg;
    public Button closeButton;
    public Button buildButton;
    public Transform grid;
    public GameObject catalogListItemPrefab;
    public GameObject detailsPanel;
    public AssignFactoryBuildCommandEvent assignFactoryBuildCommandEvent;

    private Factory factory;
    private CatalogListItem selectedCatalogListItem;

    private void Start()
    {
        buildButton.gameObject.SetActive(false);
        buildButton.onClick.AddListener(onClickBuild);
        closeButton.onClick.AddListener(onClickClose);
    }

    public void show()
    {
        detailsPanel.SetActive(false);
        gameObject.SetActive(true);
    }

    public void setFactory(Factory factory)
    {
        this.factory = factory;
        labelText.text = factory.type.name;
        factoryImg.sprite = Resources.Load<Sprite>("Images/Factories/" + factory.type.name);

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

    private void onClickClose()
    {
        detailsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void onClickBuild()
    {
        detailsPanel.SetActive(true);
        assignFactoryBuildCommandEvent.Invoke(factory, selectedCatalogListItem.getType());
        gameObject.SetActive(false);
    }

    private void onSelectCatalogItem(CatalogListItem selectedCatalogListItem)
    {
        this.selectedCatalogListItem = selectedCatalogListItem;

        CatalogListItem[] catalogListItems = GetComponentsInChildren<CatalogListItem>();
        foreach (CatalogListItem catalogListItem in catalogListItems)
        {
            if(catalogListItem.Equals(this.selectedCatalogListItem))
            {
                catalogListItem.setSelected(true);
            } else
            {
                catalogListItem.setSelected(false);
            }
        }

        buildButton.gameObject.SetActive(true);
    }
}
