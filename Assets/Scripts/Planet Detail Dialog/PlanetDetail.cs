using UnityEngine;
using TMPro;

public class PlanetDetail : MonoBehaviour
{
    public TextMeshProUGUI planetNameLabel;
    public SpriteRenderer planetImg;
    public SpriteRenderer starsImg;

    public SpriteRenderer planetShieldImg;

    public GameObject shipListItemPrefab;
    public GameObject personnelListItemPrefab;
    public GameObject factoryListItemPrefab;
    public GameObject defenseListItemPrefab;
    public GameObject energySquarePrefab;

    public Transform shipsTeamAPanel;
    public Transform shipsTeamBPanel;
    public Transform personnelTeamAPanel;
    public Transform personnelTeamBPanel;
    public Transform infrastructurePanel;


    private MainGameState gameState;
    private Planet planet;

    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.planetForDetail;
        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        starsImg.sprite = Resources.Load<Sprite>("Images/Stars/" + planet.name);
        planetNameLabel.text = planet.name;
        bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
        planetShieldImg.enabled = hasOrbitalShield;
        updateGrid();
    }

    private void updateGrid()
    {
        clearAll();

        GameObject newObj;

        // Ships
        planet.shipsInOrbit.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Ship ship in planet.shipsInOrbit)
        {
            Transform panelTransform = ship.team.Equals(Team.TeamA) ? shipsTeamAPanel : shipsTeamBPanel;
            newObj = (GameObject)Instantiate(shipListItemPrefab, panelTransform);
            ShipListItem shipListItem = newObj.GetComponent<ShipListItem>();
            shipListItem.setShip(ship);
        }

        // Personnel
        planet.personnelsOnSurface.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Personnel personnel in planet.personnelsOnSurface)
        {
            Transform panelTransform = personnel.team.Equals(Team.TeamA) ? personnelTeamAPanel : personnelTeamBPanel;
            newObj = (GameObject)Instantiate(personnelListItemPrefab, panelTransform);
            PersonnelListItem personnelListItem = newObj.GetComponent<PersonnelListItem>();
            personnelListItem.setPersonnel(personnel);
        }

        // Factories
        planet.factories.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Factory factory in planet.factories)
        {
            newObj = (GameObject)Instantiate(factoryListItemPrefab, infrastructurePanel);
            FactoryListItem factoryListItem = newObj.GetComponent<FactoryListItem>();
            factoryListItem.setFactory(factory);
        }

        // Defenses
        planet.defenses.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Defense defense in planet.defenses)
        {
            newObj = (GameObject)Instantiate(defenseListItemPrefab, infrastructurePanel);
            DefenseListItem defenseListItem = newObj.GetComponent<DefenseListItem>();
            defenseListItem.setDefense(defense);
        }

        int manyFacilities = planet.factories.Count + planet.defenses.Count;
        for (int i = manyFacilities + 1; i <= planet.energyCapacity; i++)
        {
            Instantiate(energySquarePrefab, infrastructurePanel);
        }
    }

    private void clearAll()
    {
        clearPanel(shipsTeamAPanel);
        clearPanel(shipsTeamBPanel);
        clearPanel(personnelTeamAPanel);
        clearPanel(personnelTeamBPanel);
        clearPanel(infrastructurePanel);
    }

    private void clearPanel(Transform panel)
    {
        foreach (Transform child in panel)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
