using UnityEngine;
using UnityEngine.UI;

public class ShipListItem2 : MonoBehaviour
{
    public Image shipImg;
    public Transform healthBackground;
    public Transform healthValue;

    private Ship ship;

    public void setShip(Ship ship)
    {
        this.ship = ship;
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name);
        //hasPersonnelImg.enabled = ship.personnelsOnBoard.Count > 0;

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

        //updateHealthBars();
    }
    public Ship getShip()
    {
        return ship;
    }

}
