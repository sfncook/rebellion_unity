using UnityEngine;
using UnityEngine.UI;

public class ShipContentsHeaderImage : DragAndDroppable
{
    public Image shipImg;

    private Ship ship;

    public Ship getShip()
    {
        return ship;
    }

    public void setShip(Ship ship)
    {
        this.ship = ship;
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name);
    }

    protected override bool isDraggable()
    {
        return true;
    }

    protected override bool isDroppable()
    {
        return false;
    }
}
