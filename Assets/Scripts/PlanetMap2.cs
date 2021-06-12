using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class OnClickPlanet : UnityEvent<Planet>
{
}

[System.Serializable]
public class DropGameObjectOnPlanet : UnityEvent<GameObject>
{
}

public class PlanetMap2 : DragAndDroppable
{
    public TextMeshProUGUI planetNameText;
    public Image planetImg;
    public GameObject planetaryShieldImg;
    public GameObject teamAShipsInOrbitImg;
    public GameObject teamBShipsInOrbitImg;
    public GameObject teamAPersonnelOnSurfaceImg;
    public GameObject teamBPersonnelOnSurfaceImg;
    public GameObject planetInConflictImg;
    public Image factoryImg;
    public Image defenseImg;
    public ValueBars loyaltyBars;
    public Image selectionHaloImg;

    private OnClickPlanet onClickPlanetEvent;
    private DropGameObjectOnPlanet dropGameObjectOnPlanetEvent;
    private Planet planet;

    private void Start()
    {
        MainGameState.gameState.addListenerUiUpdateEvent(onUiUpdateEvent);
    }

    public void setPlanet(Planet planet)
    {
        this.planet = planet;
        planetNameText.text = planet.name;
        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        loyaltyBars.setValue(planet.loyalty);
        onUiUpdateEvent();
    }

    public void setOnClickPlanetEvent(OnClickPlanet onClickPlanetEvent)
    {
        this.onClickPlanetEvent = onClickPlanetEvent;
    }

    public void setDropGameObjectOnPlanet(DropGameObjectOnPlanet dropGameObjectOnPlanetEvent)
    {
        this.dropGameObjectOnPlanetEvent = dropGameObjectOnPlanetEvent;
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "ShipListItem2" };
    }

    protected override bool isDraggable()
    {
        return false;
    }

    protected override bool isDroppable()
    {
        return true;
    }

    private void OnMouseUp()
    {
        selectionHaloImg.gameObject.SetActive(false);
        if (onClickPlanetEvent!=null)
        {
            onClickPlanetEvent.Invoke(planet);
        }
    }

    private void onUiUpdateEvent()
    {
        //loyaltyBars.setPlanet(planet);

        Color loyaltyColor = Color.green;
        if (planet.loyalty > 0.5f)
        {
            loyaltyColor = Color.red;
        }
        factoryImg.color = loyaltyColor;
        defenseImg.color = loyaltyColor;

        bool planetHasShipsTeamA = planet.shipsInOrbit.Exists(ship => ship.team == Team.TeamA);
        bool planetHasShipsTeamB = planet.shipsInOrbit.Exists(ship => ship.team == Team.TeamB);
        teamAShipsInOrbitImg.gameObject.SetActive(planetHasShipsTeamA);
        teamBShipsInOrbitImg.gameObject.SetActive(planetHasShipsTeamB);

        bool planetHasPersonnelTeamA = planet.personnelsOnSurface.Exists(personnel => personnel.team == Team.TeamA);
        bool planetHasPersonnelTeamB = planet.personnelsOnSurface.Exists(personnel => personnel.team == Team.TeamB);
        teamAPersonnelOnSurfaceImg.gameObject.SetActive(planetHasPersonnelTeamA);
        teamBPersonnelOnSurfaceImg.gameObject.SetActive(planetHasPersonnelTeamB);

        factoryImg.gameObject.SetActive(planet.factories.Count > 0);
        defenseImg.gameObject.SetActive(planet.defenses.Count > 0);

        int manyFacilities = planet.factories.Count + planet.defenses.Count;
        for (int i = 1; i <= manyFacilities; i++)
        {
            string strI = i.ToString().PadLeft(2, '0');
            Transform energySquare = gameObject.transform.Find("Resource Squares").Find("Square" + strI);
            energySquare.GetComponent<Image>().color = Color.blue;
            energySquare.GetComponent<Image>().enabled = true;
        }

        for (int i = manyFacilities + 1; i <= planet.energyCapacity; i++)
        {
            string strI = i.ToString().PadLeft(2, '0');
            Transform energySquare = gameObject.transform.Find("Resource Squares").Find("Square" + strI);
            energySquare.GetComponent<Image>().color = Color.white;
            energySquare.GetComponent<Image>().enabled = true;
        }

        for (int i = planet.energyCapacity + 1; i <= 10; i++)
        {
            string strI = i.ToString().PadLeft(2, '0');
            Transform energySquare = gameObject.transform.Find("Resource Squares").Find("Square" + strI);
            energySquare.GetComponent<Image>().enabled = false;
        }

        // conflict
        Dictionary<Team, int> teamToShipCount = new Dictionary<Team, int>
        {
            {Team.TeamA, 0},
            {Team.TeamB, 0}
        };
        foreach (Ship ship in planet.shipsInOrbit)
        {
            teamToShipCount[ship.team]++;
        }
        bool isInConflict = (teamToShipCount[Team.TeamA] > 0) && (teamToShipCount[Team.TeamB] > 0);
        planetInConflictImg.gameObject.SetActive(isInConflict);

        bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
        planetaryShieldImg.SetActive(hasOrbitalShield);
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        selectionHaloImg.gameObject.SetActive(true);
    }

    protected override void onPointExit()
    {
        selectionHaloImg.gameObject.SetActive(false);
    }

    protected override void onDrop(GameObject pointerDrag)
    {
        selectionHaloImg.gameObject.SetActive(false);
        if (dropGameObjectOnPlanetEvent!=null)
        {
            //bgColor.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            dropGameObjectOnPlanetEvent.Invoke(pointerDrag);
        }
    }
}
