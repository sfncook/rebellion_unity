using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

//[System.Serializable]
//public class ShowShipContentsEvent : UnityEvent<Ship>
//{
//}

//[System.Serializable]
//public class HideShipContentsEvent : UnityEvent
//{
//}

public class PlanetDetail2 : MonoBehaviour
{
    public Image planetImg;
    public SpriteRenderer starsImg;

    public GameObject shipListItemPrefab;
    public GameObject factoryListItemPrefab;
    public GameObject defenseListItemPrefab;
    public GameObject energySquarePrefab;

    public Transform shipsTeamAPanel;
    public Transform shipsTeamBPanel;
    public Transform infrastructurePanel;
    public Transform starChartPanel;
    public OnSurfacePanel onSurfacePanel;

    public ValueBars valueBars;
    public Canvas canvas;

    public GameObject detailPanel;
    public FactoryDialog factoryDialog;
    public FactoryStatusDialog factoryStatusDialog;

    public HeaderControls headerControls;

    public ShowShipContentsEvent showShipContentsEvent;
    public HideShipContentsEvent hideShipContentsEvent;



    private MainGameState gameState;
    private Planet planet;

    void Start()
    {
        detailPanel.SetActive(true);
        //factoryDialog.gameObject.SetActive(false);
        //factoryStatusDialog.gameObject.SetActive(false);

        gameState = MainGameState.gameState;
        planet = gameState.planetForDetail ?? gameState.planets[0];
        //planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        //starsImg.sprite = Resources.Load<Sprite>("Images/Stars/" + planet.name);
        headerControls.setHeaderTitle(planet.name);
        bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
        //planetShieldImg.enabled = hasOrbitalShield;
        //loyaltyBars.setPlanet(planet);
        updateGrid();
    }

    private void updateGrid()
    {

        //onSurfacePanel.setPlanet(planet);
        //onSurfacePanel.setUpdateShipGrids(updateShipGrids);
        updateShipGrids();

        //clearPanel(infrastructurePanel);

        //GameObject newObj;

        //// Defenses
        //planet.defenses.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        //foreach (Defense defense in planet.defenses)
        //{
        //    newObj = (GameObject)Instantiate(defenseListItemPrefab, infrastructurePanel);
        //    DefenseListItem defenseListItem = newObj.GetComponent<DefenseListItem>();
        //    defenseListItem.setDefense(defense);
        //    defenseListItem.GetComponent<Image>().color = (planet.loyalty > 0.5) ? Color.red : Color.green;
        //}

        //// Factories
        //planet.factories.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        //foreach (Factory factory in planet.factories)
        //{
        //    newObj = (GameObject)Instantiate(factoryListItemPrefab, infrastructurePanel);
        //    FactoryListItem factoryListItem = newObj.GetComponent<FactoryListItem>();
        //    factoryListItem.setFactory(factory);
        //    factoryListItem.GetComponent<Image>().color = (planet.loyalty > 0.5) ? Color.red : Color.green;
        //    factoryListItem.setOnClickFactoryHandler(onClickFactory);
        //}

        //int manyFacilities = planet.factories.Count + planet.defenses.Count;
        //for (int i = manyFacilities + 1; i <= planet.energyCapacity; i++)
        //{
        //    Instantiate(energySquarePrefab, infrastructurePanel);
        //}
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
            //shipListItem.setRemovePersonnelDelegate(removePersonnel);
            shipListItem.setShip(ship);
            //shipListItem.setCanvas(canvas);
            //shipListItem.setShowShipContentsEvent(showShipContentsEvent);
            //shipListItem.setStartMoveShip(startMoveShip);
            //shipListItem.setStopMoveShip(stopMoveShip);
        }
    }

    private void clearPanel(Transform panel)
    {
        foreach (Transform child in panel)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    //public void removePersonnel(Personnel personnel)
    //{
    //    planet.personnelsOnSurface.Remove(personnel);
    //    updateGrid();
    //}

    //public void removeShip(Ship ship)
    //{
    //    planet.shipsInOrbit.Remove(ship);
    //    updateGrid();
    //    hideShipContentsEvent.Invoke();
    //}

    //public void startMoveShip()
    //{
    //    starChartPanel.gameObject.SetActive(true);
    //    onSurfacePanel.gameObject.SetActive(false);
    //}

    //public void stopMoveShip()
    //{
    //    starChartPanel.gameObject.SetActive(false);
    //    onSurfacePanel.gameObject.SetActive(true);
    //}

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
