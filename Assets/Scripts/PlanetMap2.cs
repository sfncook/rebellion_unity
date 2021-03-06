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
public class DropGameObjectOnPlanet : UnityEvent<GameObject, Planet>
{
}

public class PlanetMap2 : DragAndDroppable
{

    private readonly Dictionary<Team, int> teamToShipCount = new Dictionary<Team, int>
    {
        {Team.TeamA, 0},
        {Team.TeamB, 0}
    };

    public TextMeshProUGUI planetNameText;
    public Image planetNameBg;
    public Image planetImg;
    public GameObject planetaryShieldImg;
    public GameObject teamAShipsInOrbitImg;
    public GameObject teamBShipsInOrbitImg;
    public GameObject teamAShipsInTransitImg;
    public GameObject teamBShipsInTransitImg;
    public GameObject teamAPersonnelOnSurfaceImg;
    public GameObject teamBPersonnelOnSurfaceImg;
    public GameObject planetInConflictImg;
    public GameObject resourceSquaresPanel;
    public Image factoryImg;
    public Image defenseImg;
    public ValueBars loyaltyBars;
    public Image selectionHaloImg;
    public ShipMoveStarChart shipMoveStarChart;

    private OnClickPlanet onClickPlanetEvent;
    private DropGameObjectOnPlanet dropGameObjectOnPlanetEvent;
    private Planet planet;
    private bool isSelected = false;

    private Sprite planetImgSprite;
    private Sprite circleImgSprite;

    private void Start()
    {
        MainGameState.gameState.addListenerUiUpdateEvent(onUiUpdateEvent);
    }

    public void setPlanet(Planet planet)
    {
        this.planet = planet;
        planetNameText.text = planet.name;
        planetImgSprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        circleImgSprite = Resources.Load<Sprite>("Images/circle");
        loyaltyBars.setValue(planet.loyalty);
        onUiUpdateEvent();
    }
    public Planet getPlanet()
    {
        return planet;
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
        loyaltyBars.setValue(planet.loyalty);

        Color loyaltyColor = planet.getTeam().getColorForTeam();
        factoryImg.color = loyaltyColor;
        defenseImg.color = loyaltyColor;
        //if(planet.isDiscovered)
        if(true)
        {
            planetImg.color = Color.white;
            planetImg.sprite = planetImgSprite;
            planetNameBg.color = new Color(loyaltyColor.r, loyaltyColor.g, loyaltyColor.b, 0.5f);

            loyaltyBars.gameObject.SetActive(true);
            resourceSquaresPanel.SetActive(true);

            bool planetHasShipsTeamA = planet.shipsInOrbit.Exists(ship => ship.team == Team.TeamA);
            bool planetHasShipsTeamB = planet.shipsInOrbit.Exists(ship => ship.team == Team.TeamB);
            teamAShipsInOrbitImg.gameObject.SetActive(planetHasShipsTeamA);
            teamBShipsInOrbitImg.gameObject.SetActive(planetHasShipsTeamB);

            teamAShipsInTransitImg.gameObject.SetActive(planet.shipsInTransit.Exists(ship => ship.team == Team.TeamA));
            teamBShipsInTransitImg.gameObject.SetActive(planet.shipsInTransit.Exists(ship => ship.team == Team.TeamB));

            bool planetHasPersonnelTeamA = planet.personnelsOnSurface.Exists(personnel => personnel.team == Team.TeamA);
            bool planetHasPersonnelTeamB = planet.personnelsOnSurface.Exists(personnel => personnel.team == Team.TeamB);
            teamAPersonnelOnSurfaceImg.gameObject.SetActive(planetHasPersonnelTeamA);
            teamBPersonnelOnSurfaceImg.gameObject.SetActive(planetHasPersonnelTeamB);

            factoryImg.gameObject.SetActive(planet.factories.Count > 0);
            defenseImg.gameObject.SetActive(planet.defenses.Count > 0);

            resourceSquaresPanel.SetActive(true);
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
            foreach (Ship ship in planet.shipsInOrbit)
            {
                teamToShipCount[ship.team]++;
            }
            bool isInConflict = (teamToShipCount[Team.TeamA] > 0) && (teamToShipCount[Team.TeamB] > 0);
            planetInConflictImg.gameObject.SetActive(isInConflict);

            bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
            planetaryShieldImg.SetActive(hasOrbitalShield);
        } else
        {
            planetImg.color = new Color(0.5f, 0.5f, 0.5f, 0.9f);
            planetImg.sprite = circleImgSprite;
            planetNameBg.color = new Color(1f, 1f, 1f, 0.5f);

            planetaryShieldImg.gameObject.SetActive(false);
            teamAShipsInOrbitImg.gameObject.SetActive(false);
            teamBShipsInOrbitImg.gameObject.SetActive(false);
            bool exists = planet.shipsInTransit.Exists(ship => ship.team == MainGameState.gameState.myTeam);
            teamAShipsInTransitImg.gameObject.SetActive(planet.shipsInTransit.Exists(ship => ship.team == MainGameState.gameState.myTeam));
            teamBShipsInTransitImg.gameObject.SetActive(false);
            teamAPersonnelOnSurfaceImg.gameObject.SetActive(false);
            teamBPersonnelOnSurfaceImg.gameObject.SetActive(false);
            planetInConflictImg.gameObject.SetActive(false);
            factoryImg.gameObject.SetActive(false);
            defenseImg.gameObject.SetActive(false);
            loyaltyBars.gameObject.SetActive(false);
            resourceSquaresPanel.SetActive(false);
        }
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
            dropGameObjectOnPlanetEvent.Invoke(pointerDrag, planet);
        }
    }

    public void setIsSelected(bool isSelected)
    {
        selectionHaloImg.gameObject.SetActive(true);
        this.isSelected = isSelected;
    }
}
