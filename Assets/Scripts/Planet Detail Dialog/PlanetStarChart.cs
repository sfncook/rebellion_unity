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

    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.getPlanetByName(gameObject.name);

        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        planetNameLabel.text = planet.name;
        loyaltyBars.setPlanet(planet);
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "ShipListItem" };
    }

    protected override bool isDraggable()
    {
        return false;
    }

    protected override bool isDroppable()
    {
        return true;
    }

    protected override void onDrop(GameObject pointerDrag)
    {
        Debug.Log("onDrop");
        hoverGlow.gameObject.SetActive(false);
        //hoverGlow.gameObject.SetActive(false);
        //ShipContentsHeaderImage shipListItem = pointerDrag.GetComponent<ShipContentsHeaderImage>();
        //Ship ship = shipListItem.getShip();
        //if (gameState.myTeam == ship.team)
        //{
        //    removeShipCallback.Invoke(ship);
        //    planet.shipsInOrbit.Add(ship);
        //}
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        hoverGlow.gameObject.SetActive(true);
        //ShipContentsHeaderImage shipListItem = pointerDrag.GetComponent<ShipContentsHeaderImage>();
        //if(gameState.myTeam == shipListItem.getShip().team)
        //{
        //    hoverGlow.gameObject.SetActive(true);
        //}
    }

    protected override void onPointExit()
    {
        hoverGlow.gameObject.SetActive(false);
    }

}
