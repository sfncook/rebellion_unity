using UnityEngine;
using TMPro;

public class ShipListItem : MonoBehaviour
{
    public SpriteRenderer shipImgSpriteRenderer;
    public TextMeshProUGUI shipNameLabel;
    public Sprite shipImg2;
    public Sprite shipImg3;
    public Sprite shipImg4;
    public Sprite shipImg5;

    public void setShip(Ship ship)
    {
        Sprite shipImg;
        if(ship.type.Equals(ShipType.Bireme))
        {
            shipImg = shipImg2;
        } else if (ship.type.Equals(ShipType.Trireme))
        {
            shipImg = shipImg3;
        }
        else if (ship.type.Equals(ShipType.Quadreme))
        {
            shipImg = shipImg4;
        }
        else if (ship.type.Equals(ShipType.Quintreme))
        {
            shipImg = shipImg5;
        } else
        {
            Debug.Log("else");
            shipImg = shipImg2;
        }

        shipImgSpriteRenderer.sprite = shipImg;
        shipNameLabel.text = ship.type.name;

    }
}
