using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipContentsPanel : MonoBehaviour
{
    public GameObject shipsInOrbitPanel;
    public Image shipImg;
    public TextMeshProUGUI shipTypeNameLabel;


    public void showShipContents(Ship ship)
    {
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name);
        shipTypeNameLabel.text = ship.type.name;
        shipsInOrbitPanel.SetActive(false);
        gameObject.SetActive(true);
    }

    public void hideShipContents()
    {
        shipsInOrbitPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
