using UnityEngine;

public class ShipListItem : MonoBehaviour
{
    public SpriteRenderer shipImg;
    public SpriteRenderer hasPersonnelImg;

    public void setShip(Ship ship)
    {
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name);
        hasPersonnelImg.enabled = false;

        if (ship.team.Equals(Team.TeamA))
        {
            shipImg.transform.localScale = new Vector3(100, 100, 1);
            shipImg.color = Color.green;
        }
        else
        {
            shipImg.transform.localScale = new Vector3(-100, 100, 1);
            shipImg.color = Color.red;
        }
    }
}
