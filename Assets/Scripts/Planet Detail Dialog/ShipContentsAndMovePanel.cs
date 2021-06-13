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

    private Ship ship;

    public void showShipContents(Ship ship)
    {
        this.ship = ship;
        shipTypeNameLabel.text = ship.type.name;
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name);

        updateGrid();

        inOrbitPanel.SetActive(false);
        onSurfacePanel.SetActive(true);
        gameObject.SetActive(true);
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
            personnelListItem.setLocatedOnShip(true);
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
        return true;
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
