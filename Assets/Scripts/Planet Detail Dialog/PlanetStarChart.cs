using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class RemoveShipEvent : UnityEvent<Ship>
{
}

public class PlanetStarChart : Droppable
{
    public TextMeshProUGUI planetNameLabel;
    public Image planetImg;
    public LoyaltyBars loyaltyBars;
    public Image hoverGlow;
    public RemoveShipEvent removeShipCallback;

    private MainGameState gameState;
    private Planet planet;

    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.getPlanetByName(gameObject.name);

        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        planetNameLabel.text = planet.name;
        loyaltyBars.setPlanet(planet);

        // Ships cannot be "moved" to the same planet where they already are located
        if (planet.Equals(gameState.planetForDetail))
        {
            Color color = new Color(0.6f, 0.6f, 0.6f, 0.6f);
            planetImg.color = color;
            planetNameLabel.color = color;
            loyaltyBars.gameObject.SetActive(false);
        }
    }

    protected override List<string> acceptedDropTypes()
    {
        // Ships cannot be "moved" to the same planet where they already are located
        if(!planet.Equals(gameState.planetForDetail))
        {
            return new List<string>() { "ShipListItem" };
        } else
        {
            return new List<string>() { };
        }
    }

    protected override void onDrop(GameObject pointerDrag)
    {
        hoverGlow.gameObject.SetActive(false);
        ShipListItem shipListItem = pointerDrag.GetComponent<ShipListItem>();
        Ship ship = shipListItem.getShip();
        removeShipCallback.Invoke(ship);
        planet.shipsInOrbit.Add(ship);
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        hoverGlow.gameObject.SetActive(true);
    }

    protected override void onPointExit()
    {
        hoverGlow.gameObject.SetActive(false);
    }

}
