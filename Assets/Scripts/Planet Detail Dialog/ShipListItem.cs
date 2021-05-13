using UnityEngine;

public class ShipListItem : MonoBehaviour
{
    private Ship ship;

    public void setShip(Ship ship)
    {
        this.ship = ship;
        transform.Find("Ship Img");
    }
}
