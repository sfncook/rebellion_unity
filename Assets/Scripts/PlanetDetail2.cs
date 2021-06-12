using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class ShowShipContentsEvent2 : UnityEvent<Ship>
{
}

[System.Serializable]
public class HideShipContentsEvent2 : UnityEvent
{
}

public class PlanetDetail2 : MonoBehaviour
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
    public Transform starChartPanel;
    public OnSurfacePanel onSurfacePanel;

    public ValueBars loyaltyBars;
    public Canvas canvas;

    public GameObject detailPanel;
    public FactoryDialog factoryDialog;
    public FactoryStatusDialog factoryStatusDialog;

    public HeaderControls headerControls;

    public ShowShipContentsEvent2 showShipContentsEvent;
    public HideShipContentsEvent2 hideShipContentsEvent;



    private MainGameState gameState;
    private Planet planet;

    void Start()
    {
        detailPanel.SetActive(true);
        //factoryDialog.gameObject.SetActive(false);
        //factoryStatusDialog.gameObject.SetActive(false);

        gameState = MainGameState.gameState;
        planet = gameState.planetForDetail;
        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        starsBackgroundImg.sprite = Resources.Load<Sprite>("Images/Stars/" + gameState.sectorForDetail.name);
        headerControls.setHeaderTitle(planet.name);
        bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
        planetShieldImg.gameObject.SetActive(hasOrbitalShield);
        loyaltyBars.setValue(planet.loyalty);
        updateGrid();
    }

    void FixedUpdate()
    {
        if(planet != null)
        {
            bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
            if (hasOrbitalShield)
            {
                float r = Random.Range(0f, 0.1f);
                float g = Random.Range(0f, 0.1f);
                float b = Random.Range(0.9f, 1f);
                float a = Random.Range(0.1f, 0.3f);
                planetShieldImg.color = new Color(r, g, b, a);
            }
        }
    }

    private void updateGrid()
    {

        onSurfacePanel.setPlanet(planet);
        //onSurfacePanel.setUpdateShipGrids(updateShipGrids);
        updateShipGrids();

        clearPanel(infrastructurePanel);

        GameObject newObj;

        // Defenses
        planet.defenses.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Defense defense in planet.defenses)
        {
            newObj = (GameObject)Instantiate(defenseListItemPrefab, infrastructurePanel);
            DefenseListItem2 defenseListItem = newObj.GetComponent<DefenseListItem2>();
            defenseListItem.setDefense(defense, Team.TeamA);
        }

        // Factories
        planet.factories.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Factory factory in planet.factories)
        {
            newObj = (GameObject)Instantiate(factoryListItemPrefab, infrastructurePanel);
            FactoryListItem2 factoryListItem = newObj.GetComponent<FactoryListItem2>();
            factoryListItem.setFactory(factory, Team.TeamA);
            //factoryListItem.setOnClickFactoryHandler(onClickFactory);
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

        GameObject newObj;

        // Ships
        planet.shipsInOrbit.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Ship ship in planet.shipsInOrbit)
        {
            Transform panelTransform = ship.team.Equals(Team.TeamA) ? shipsTeamAPanel : shipsTeamBPanel;
            newObj = (GameObject)Instantiate(shipListItemPrefab, panelTransform);
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

    public void removeShip(Ship ship)
    {
        planet.shipsInOrbit.Remove(ship);
        updateGrid();
        hideShipContentsEvent.Invoke();
    }

    public void startMoveShip()
    {
        //starChartPanel.gameObject.SetActive(true);
        onSurfacePanel.gameObject.SetActive(false);
    }

    public void stopMoveShip()
    {
        //starChartPanel.gameObject.SetActive(false);
        onSurfacePanel.gameObject.SetActive(true);
    }

    //public void assignFactoryBuildCommand(Factory factory, AbstractType type)
    //{
    //    factory.isBuilding = true;
    //    factory.buildingType = type;
    //    factory.buildingDoneDay = gameState.gameTime + type.daysToBuild;
    //    updateGrid();
    //}

    //public void cancelFactoryBuildCommand(Factory factory)
    //{
    //    factory.isBuilding = false;
    //    factory.buildingType = null;
    //    updateGrid();
    //    detailPanel.SetActive(true);
    //    factoryDialog.gameObject.SetActive(false);
    //    factoryStatusDialog.gameObject.SetActive(false);
    //}

    //public void onClickFactory(Factory factory)
    //{
    //    if (planet.loyalty < 0.5f)
    //    {
    //        if (factory.isBuilding)
    //        {
    //            factoryStatusDialog.setFactory(factory);
    //            factoryStatusDialog.show();
    //        }
    //        else
    //        {
    //            factoryDialog.setFactory(factory);
    //            factoryDialog.show();
    //        }
    //    }
    //}

    public void onClickBackButton()
    {
        MainGameState.gameState.planetForDetail = null;
        SceneManager.LoadScene("Sector Map 2");
    }
}
