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
    private bool isDraggingPersonnel = false;
    private bool isDraggingShip = false;

    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.getPlanetByName(gameObject.name);

        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        planetNameLabel.text = planet.name;
        loyaltyBars.setPlanet(planet);
    }

    public void startDraggingPersonnel()
    {
        // Personnel can only be moved to the same planet where they are currently located
        if (planet.Equals(gameState.planetForDetail))
        {
            isDraggingPersonnel = true;
            setEnabledColor();
        }
        else
        {
            isDraggingPersonnel = false;
            setDisabledColor();
        }
    }
    public void stopDraggingPersonnel()
    {
        setEnabledColor();
        //Debug.Log("stopDraggingPersonnel");
        //// Ships can only be moved to a different planet from where they are currently located
        //if (planet.Equals(gameState.planetForDetail))
        //{
        //    isDraggingShip = false;
        //    setDisabledColor();
        //}
        //else
        //{
        //    isDraggingShip = true;
        //    setEnabledColor();
        //}
    }

    public void setEnabledColor()
    {
        Color planetOverlayColor = new Color(1f, 1f, 1f, 1f);
        bool loyaltyBarsEnabled = true;
        planetImg.color = planetOverlayColor;
        planetNameLabel.color = planetOverlayColor;
        loyaltyBars.gameObject.SetActive(loyaltyBarsEnabled);
    }
    public void setDisabledColor()
    {
        Color planetOverlayColor = new Color(0.6f, 0.6f, 0.6f, 0.6f);
        bool loyaltyBarsEnabled = false;
        planetImg.color = planetOverlayColor;
        planetNameLabel.color = planetOverlayColor;
        loyaltyBars.gameObject.SetActive(loyaltyBarsEnabled);
    }


    protected override List<string> acceptedDropTypes()
    {
        if (isDraggingPersonnel)
        {
            return new List<string>() { "PersonnelListItem" };
        } else if(isDraggingShip)
        {
            return new List<string>() { "ShipContentsHeaderImage" };
        } else
        {
            return new List<string>() { };
        }
    }

    protected override void onDrop(GameObject pointerDrag)
    {
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
