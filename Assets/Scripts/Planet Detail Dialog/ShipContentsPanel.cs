using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipContentsPanel : MonoBehaviour
{
    public GameObject shipsInOrbitPanel;
    public Image shipImg;
    public TextMeshProUGUI shipTypeNameLabel;
    public Transform shipContentsGrid;
    public GameObject personnelListItemPrefab;
    public Canvas canvas;

    private Ship ship;


    public void showShipContents(Ship ship)
    {
        this.ship = ship;
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name);
        shipTypeNameLabel.text = ship.type.name;
        updateGrid();

        shipsInOrbitPanel.SetActive(false);
        gameObject.SetActive(true);
    }

    public void hideShipContents()
    {
        shipsInOrbitPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void updateGrid()
    {
        clearGrid();

        GameObject newObj;
        ship.personnelsOnBoard.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Personnel personnel in ship.personnelsOnBoard)
        {
            newObj = (GameObject)Instantiate(personnelListItemPrefab, shipContentsGrid);
            PersonnelListItem personnelListItem = newObj.GetComponent<PersonnelListItem>();
            personnelListItem.setPersonnel(personnel);
            personnelListItem.setCanvas(canvas);
        }
    }

    private void clearGrid()
    {
        foreach (Transform child in shipContentsGrid)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
