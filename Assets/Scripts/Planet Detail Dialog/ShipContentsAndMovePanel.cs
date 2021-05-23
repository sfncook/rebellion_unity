using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShipContentsAndMovePanel : DragAndDroppable
{
    public GameObject inOrbitPanel;
    public GameObject onSurfacePanel;
    public TextMeshProUGUI shipTypeNameLabel;
    public Transform shipContentsGrid;
    public GameObject personnelListItemPrefab;
    public Image shipImg;
    public Image backgroundImage;
    public PlanetDetail planetDetail;

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
            PersonnelListItem personnelListItem = newObj.GetComponent<PersonnelListItem>();
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
        return ship.team.Equals(MainGameState.gameState.myTeam);
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem" };
    }
    protected override void onDrop(GameObject pointerDrag)
    {
        backgroundImage.color = Color.black;
        PersonnelListItem personnelListItem = pointerDrag.GetComponent<PersonnelListItem>();
        Personnel personnel = personnelListItem.getPersonnel();
        ship.personnelsOnBoard.Add(personnel);
        planetDetail.removePersonnel(personnel);
        updateGrid();
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
