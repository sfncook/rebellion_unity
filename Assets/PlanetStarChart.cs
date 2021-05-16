using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetStarChart : Droppable
{
    public TextMeshProUGUI planetNameLabel;
    public Image planetImg;

    private MainGameState gameState;
    private Planet planet;

    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.getPlanetByName(gameObject.name);

        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        planetNameLabel.text = planet.name;
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "ShipListItem" };
    }

    protected override void onDrop(GameObject pointerDrag)
    {
        Debug.Log("onDrop "+planet.name);
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        Debug.Log("onPointEnter " + planet.name);
    }

    protected override void onPointExit()
    {
        Debug.Log("onPointExit " + planet.name);
    }

}
