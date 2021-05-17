using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class RemovePersonnel : UnityEvent<Personnel>
{
}

public class ShipContentsPanel : DragAndDroppable
{
    public GameObject shipsInOrbitPanel;
    public Image shipImg;
    public TextMeshProUGUI shipTypeNameLabel;
    public Transform shipContentsGrid;
    public GameObject personnelListItemPrefab;
    public Canvas canvas;
    public Image bgColor;

    public RemovePersonnel removePersonnelCallback;

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

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem" };
    }


    protected override void onDrop(GameObject pointerDrag)
    {
        PersonnelListItem personnelListItem = pointerDrag.GetComponent<PersonnelListItem>();
        if (
            personnelListItem.getPersonnel().team == ship.team &&
            ship.personnelsOnBoard.Count < ((ShipType)ship.type).personnelCapacity
            )
        {
            personnelListItem.setIsValidDrop(true);
            ship.personnelsOnBoard.Add(personnelListItem.getPersonnel());
            removePersonnelCallback.Invoke(personnelListItem.getPersonnel());
            bgColor.color = new Color(0.18f, 0.18f, 0.18f, 1.0f);
            updateGrid();
        }
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        bgColor.color = Color.yellow;
    }

    protected override void onPointExit()
    {
        bgColor.color = new Color(0.18f, 0.18f, 0.18f, 1.0f);
    }

    protected override bool isDraggable()
    {
        return false;
    }

    protected override bool isDroppable()
    {
        return true;
    }
}
