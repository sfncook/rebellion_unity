using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class ShowShipContentsEvent2 : UnityEvent<Ship>
{
}

[System.Serializable]
public class HideShipContentsEvent2 : UnityEvent
{
}

public class PlanetDetail2 : BladeFadeScene
{
    public Image planetImg;
    public Image planetShieldImg;
    public Image starsBackgroundImg;

    public GameObject shipListItemPrefab;
    public GameObject factoryListItemPrefab;
    public GameObject defenseListItemPrefab;
    public GameObject energySquarePrefab;

    public Transform shipsTeamAPanel;
    public Transform shipsTeamBPanel;
    public Transform infrastructurePanel;
    public ShipMoveStarChart shipMoveStarChart;
    public OnSurfacePanel onSurfacePanel;

    public MissionAssignmentDialog missionAssignmentDialog;

    public ValueBars loyaltyBars;
    public TextMeshProUGUI teamALoyaltyText;
    public TextMeshProUGUI teamBLoyaltyText;

    public Canvas canvas;

    public GameObject detailPanel;
    //public FactoryDialog factoryDialog;
    public FactoryStatusDialog factoryStatusDialog;

    public HeaderControls headerControls;

    public ShowShipContentsEvent2 showShipContentsEvent;
    public HideShipContentsEvent2 hideShipContentsEvent;



    private MainGameState gameState;
    private Planet planet;

    void Start()
    {
        if(MainGameState.gameState.newGameFadeIn)
        {
            fadein = true;
            blackFade.color = Color.black;
        } else
        {
            blackFade.color = Color.clear;
        }
        MainGameState.gameState.newGameFadeIn = false;

        //headerControls.setShowBackButton(MainGameState.gameState.showPlanetDetailBackButton);

        detailPanel.SetActive(true);
        //factoryDialog.gameObject.SetActive(false);
        factoryStatusDialog.gameObject.SetActive(false);

        onSurfacePanel.setUpdateShipGrids(updateShipGrids);

        gameState = MainGameState.gameState;
        planet = gameState.planetForDetail;
        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        starsBackgroundImg.sprite = Resources.Load<Sprite>("Images/Stars/" + gameState.sectorForDetail.name);
        headerControls.setHeaderTitle(planet.name);
        bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
        planetShieldImg.gameObject.SetActive(hasOrbitalShield);
        loyaltyBars.setValue(planet.loyalty);
        teamALoyaltyText.text = ((int)(planet.loyalty * 100f)).ToString();
        teamBLoyaltyText.text = ((int)((1f - planet.loyalty) * 100f)).ToString();
        updateGrid();

        gameState.addListenerUiUpdateEvent(updateGrid);
        gameState.showMissionAssignmentDialog.AddListener(onShowMissionAssignmentDialog);

        missionAssignmentDialog.setUpdatePlanetDetail(updateGrid);
    }

    void FixedUpdate()
    {
        _FixedUpdate();
        if (planet != null)
        {
            loyaltyBars.setValue(planet.loyalty);
            teamALoyaltyText.text = ((int)(planet.loyalty * 100f)).ToString();
            teamBLoyaltyText.text = ((int)((1f - planet.loyalty) * 100f)).ToString();

            // Orbital shield color-flicker.  Not very helpful
            //bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
            //if (hasOrbitalShield)
            //{
            //    float r = Random.Range(0f, 0.1f);
            //    float g = Random.Range(0f, 0.1f);
            //    float b = Random.Range(0.9f, 1f);
            //    float a = Random.Range(0.1f, 0.3f);
            //    planetShieldImg.color = new Color(r, g, b, a);
            //}
        }
    }

    private void updateGrid()
    {
        onSurfacePanel.setPlanet(planet);
        updateShipGrids();

        clearPanel(infrastructurePanel);

        GameObject newObj;

        // Defenses
        planet.defenses.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Defense defense in planet.defenses.Concat(planet.defensesInTransit))
        {
            newObj = (GameObject)Instantiate(defenseListItemPrefab, infrastructurePanel);
            DefenseListItem2 defenseListItem = newObj.GetComponent<DefenseListItem2>();
            defenseListItem.setDefense(defense, planet.getTeam());
        }

        // Factories
        planet.factories.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        List<Factory> allFactories = planet.factories.Concat(planet.factoriesInTransit).ToList<Factory>();
        //Debug.Log("allFactories:" + allFactories.Count);
        foreach (Factory factory in allFactories)
        {
            newObj = (GameObject)Instantiate(factoryListItemPrefab, infrastructurePanel);
            FactoryListItem2 factoryListItem = newObj.GetComponent<FactoryListItem2>();
            factoryListItem.setFactory(factory, planet.getTeam());
            factoryListItem.setOnClickFactoryHandler(onClickFactory);
        }

        int manyFacilities = planet.factories.Count + planet.defenses.Count;
        for (int i = manyFacilities + 1; i <= planet.energyCapacity; i++)
        {
            Instantiate(energySquarePrefab, infrastructurePanel);
        }
    }

    private void updateShipGrids()
    {
        clearPanel(shipsTeamAPanel);
        clearPanel(shipsTeamBPanel);

        

        // Ships
        planet.shipsInOrbit.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Ship ship in planet.shipsInOrbit.Concat(planet.shipsInTransit))
        {
            Transform panelTransform = ship.team.Equals(Team.TeamA) ? shipsTeamAPanel : shipsTeamBPanel;
            GameObject newObj = (GameObject)Instantiate(shipListItemPrefab, panelTransform);
            ShipListItem2 shipListItem = newObj.GetComponent<ShipListItem2>();
            shipListItem.setRemovePersonnelDelegate(removePersonnel);
            shipListItem.setShip(ship);
            shipListItem.setCanvas(canvas);
            shipListItem.setShowShipContentsEvent(showShipContentsEvent);
            shipListItem.setStartMoveShip(startMoveShip);
            shipListItem.setStopMoveShip(stopMoveShip);
        }
    }

    private void clearPanel(Transform panel)
    {
        foreach (Transform child in panel)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void removePersonnel(Personnel personnel)
    {
        planet.personnelsOnSurface.Remove(personnel);
        updateGrid();
    }

    public void startMoveShip(ShipListItem2 shipListItem)
    {
        shipListItem.transform.SetParent(canvas.transform);
        shipMoveStarChart.showSector(MainGameState.gameState.sectorForDetail);
        shipMoveStarChart.gameObject.SetActive(true);
        detailPanel.gameObject.SetActive(false);
    }

    public void stopMoveShip(ShipListItem2 shipListItem)
    {
        GameObject.Destroy(shipListItem.gameObject);
        shipMoveStarChart.gameObject.SetActive(false);
        detailPanel.gameObject.SetActive(true);
        updateGrid();
    }

    public void assignFactoryBuildCommand(Factory factory, AbstractType type)
    {
        factory.isBuilding = true;
        factory.buildingType = type;
        factory.buildingDoneDay = gameState.gameTime + type.daysToBuild;
        updateGrid();
    }

    public void cancelFactoryBuildCommand(Factory factory)
    {
        factory.isBuilding = false;
        factory.buildingType = null;
        updateGrid();
        detailPanel.SetActive(true);
        //factoryDialog.gameObject.SetActive(false);
        factoryStatusDialog.gameObject.SetActive(false);
    }

    public void onClickFactory(Factory factory)
    {
        if (planet.getTeam().Equals(MainGameState.gameState.myTeam))
        {
            if (factory.isBuilding)
            {
                gameState.factoryForDetail = factory;
                SceneManager.LoadScene("Factory Status Dialog 2");
            }
            else
            {
                gameState.factoryForDetail = factory;
                SceneManager.LoadScene("Factory Build Dialog 2");
            }
        }
    }

    public void onClickBackButton()
    {
        MainGameState.gameState.planetForDetail = null;
        SceneManager.LoadScene("Sector Map 2");
    }

    public void dropGameObjectOnPlanetEvent(GameObject pointerDrag, Planet destPlanet)
    {
        //Debug.Log("dropGameObjectOnPlanetEvent destPlanet:"+destPlanet.name);
        ShipListItem2 shipListItem = pointerDrag.GetComponent<ShipListItem2>();
        Ship ship = shipListItem.getShip();
        planet.shipsInOrbit.Remove(ship);
        planet.shipsInTransit.Remove(ship);
        ship.dayArrival = MainGameState.arrivalDay(planet, destPlanet);
        destPlanet.shipsInTransit.Add(ship);
        updateShipGrids();
    }

    private void onShowMissionAssignmentDialog(
        Personnel personnel,
        AbstractUnit missionTargetUnit,
        Planet missionTargetPlanet
    )
    {
        missionAssignmentDialog.setMissionParameters(personnel, missionTargetUnit, missionTargetPlanet);
        missionAssignmentDialog.gameObject.SetActive(true);
    }
}
