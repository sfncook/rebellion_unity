using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class RemoveShipEvent : UnityEvent<Ship>
{
}

public class PlanetStarChart : DragAndDroppable
{
    public TextMeshProUGUI planetNameLabel;
    public Image planetImg;
    public LoyaltyBars loyaltyBars;
    public Image hoverGlow;
    public RemoveShipEvent removeShipCallback;

    private MainGameState gameState;
    private Planet planet;
    private bool isSamePlanet = false;

    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.getPlanetByName(gameObject.name);

        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        planetNameLabel.text = planet.name;
        loyaltyBars.setPlanet(planet);
        isSamePlanet = gameState.planetForDetail == planet;
        if(isSamePlanet)
        {
            planetImg.color = new Color(0.6f, 0.6f, 0.6f, 0.6f);
        }
    }

    protected override List<string> acceptedDropTypes()
    {
        if(!isSamePlanet)
        {
            return new List<string>() { "ShipListItem" };
        } else
        {
            return new List<string>() { };
        }
    }

    protected override bool isDraggable()
    {
        return false;
    }

    protected override bool isDroppable()
    {
        return !isSamePlanet;
    }

    protected override void onDrop(GameObject pointerDrag)
    {
        if(!isSamePlanet)
        {
            hoverGlow.gameObject.SetActive(false);
            ShipListItem shipListItem = pointerDrag.GetComponent<ShipListItem>();
            Ship ship = shipListItem.getShip();
            if (gameState.myTeam == ship.team)
            {
                removeShipCallback.Invoke(ship);
                planet.shipsInOrbit.Add(ship);
            }
        }
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        if (!isSamePlanet) { 
            ShipListItem shipListItem = pointerDrag.GetComponent<ShipListItem>();
            if (gameState.myTeam == shipListItem.getShip().team)
            {
                hoverGlow.gameObject.SetActive(true);
            }
        }
    }

    protected override void onPointExit()
    {
        hoverGlow.gameObject.SetActive(false);
    }

}
