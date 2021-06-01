using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FactoryDialog : MonoBehaviour
{
    public TextMeshProUGUI labelText;
    public Image factoryImg;
    public Button closeButton;
    public Transform grid;
    public GameObject catalogListItemPrefab;

    private Factory factory;

    private void Start()
    {
        closeButton.onClick.AddListener(onClickClose);
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
        gameObject.SetActive(false);
    }
}
