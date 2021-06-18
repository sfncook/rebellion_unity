using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ShipContentsAndMovePanel : DragAndDroppable
{
    public GameObject inOrbitPanel;
    public GameObject onSurfacePanel;
    public TextMeshProUGUI shipTypeNameLabel;
    public Transform shipContentsGrid;
    public GameObject personnelListItemPrefab;
    public Image shipImg;
    public Image backgroundImage;
    public PlanetDetail2 planetDetail;
    public Image inTransitImg;
    public GameObject inTransitPanel;
    public TextMeshProUGUI inTransitArrivalDayText;

    private Ship ship;

    private void Start()
    {
        MainGameState.gameState.addListenerUiUpdateEvent(updateContents);
    }

    private void updateContents()
    {
        if (ship != null && gameObject.activeSelf)
        {
            inTransitImg.gameObject.SetActive(ship.inTransit());
            inTransitPanel.gameObject.SetActive(ship.inTransit());
            Color panelColor;
            if (ship.inTransit())
            {
                panelColor = new Color(0.1058824f, 0.9843137f, 1);
                inTransitArrivalDayText.text = ship.dayArrival.ToString();
            }
            else
            {
                panelColor = new Color(1, 1, 1, 0.3921569f);
            }
            shipContentsGrid.GetComponent<Image>().color = panelColor;
        }
    }

    public void showShipContents(Ship ship)
    {
        this.ship = ship;
        shipTypeNameLabel.text = ship.type.name;
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name +"_flat");

        updateGrid();

        inOrbitPanel.SetActive(false);
        onSurfacePanel.SetActive(true);
        gameObject.SetActive(true);

        updateContents();
    }

    public void hideShipContents()
    {
        inOrbitPanel.SetActive(true);
        onSurfacePanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void updateGrid()
    {
        clearGrid();

        GameObject newObj;
        ship.personnelsOnBoard.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Personnel personnel in ship.personnelsOnBoard)
        {
            newObj = (GameObject)Instantiate(personnelListItemPrefab, shipContentsGrid);
            PersonnelListItem2 personnelListItem = newObj.GetComponent<PersonnelListItem2>();
            personnelListItem.setPersonnel(personnel);
            personnelListItem.setCanvas(canvas);
            personnelListItem.setOnShip(ship);
        }
    }

    private void clearGrid()
    {
        foreach (Transform child in shipContentsGrid)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void removePersonnel(Personnel personnel)
    {
        ship.personnelsOnBoard.Remove(personnel);
    }

    protected override bool isDraggable()
    {
        return false;
    }
    protected override bool isDroppable()
    {
        return !ship.inTransit();
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem2" };
    }
    protected override void onDrop(GameObject pointerDrag)
    {
        backgroundImage.color = Color.black;
        PersonnelListItem2 personnelListItem = pointerDrag.GetComponent<PersonnelListItem2>();
        Personnel personnel = personnelListItem.getPersonnel();
        if (!ship.personnelsOnBoard.Any(_personnel => _personnel.uuid == personnel.uuid))
        {
            ship.personnelsOnBoard.Add(personnel);
            planetDetail.removePersonnel(personnel);
            updateGrid();
        }
    }
    protected override void onPointEnter(GameObject pointerDrag)
    {
        backgroundImage.color = Color.yellow;
    }
    protected override void onPointExit()
    {
        backgroundImage.color = Color.black;
    }
}
