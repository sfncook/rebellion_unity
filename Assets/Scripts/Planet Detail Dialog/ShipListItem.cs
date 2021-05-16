using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShipListItem : Droppable
{
    public SpriteRenderer shipImg;
    public SpriteRenderer hasPersonnelImg;
    public Image bgColor;

    private Ship ship;

    public delegate void RemovePersonnel(Personnel personnel);
    private RemovePersonnel removePersonnelCallback;


    public void setRemovePersonnelDelegate(RemovePersonnel removePersonnel)
    {
        this.removePersonnelCallback = removePersonnel;
    }

    public void setShip(Ship ship)
    {
        this.ship = ship;
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name);
        hasPersonnelImg.enabled = ship.personnelsOnBoard.Count > 0;

        if (ship.team.Equals(Team.TeamA))
        {
            shipImg.color = Color.green;
        }
        else
        {
            shipImg.transform.localScale = new Vector3(
                -shipImg.transform.localScale.x,
                shipImg.transform.localScale.y,
                shipImg.transform.localScale.z
                );
            shipImg.color = Color.red;
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
            removePersonnelCallback(personnelListItem.getPersonnel());
        }
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        PersonnelListItem personnelListItem = pointerDrag.GetComponent<PersonnelListItem>();
        if (
                personnelListItem.getPersonnel().team == ship.team &&
                ship.personnelsOnBoard.Count < ((ShipType)ship.type).personnelCapacity
                )
        {
            bgColor.color = new Color(1.0f, 1.0f, 0.5f, 0.6f);
        }
    }

    protected override void onPointExit()
    {
        bgColor.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }
}
