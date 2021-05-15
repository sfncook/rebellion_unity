using UnityEngine;
using TMPro;

public class PlanetDetail : MonoBehaviour
{
    public TextMeshProUGUI planetNameLabel;
    public SpriteRenderer planetImg;
    public SpriteRenderer planetShieldImg;

    public GameObject shipListItemPrefab;
    public GameObject personnelListItemPrefab;

    public Transform shipsTeamAPanel;
    public Transform shipsTeamBPanel;
    public Transform personnelTeamAPanel;
    public Transform personnelTeamBPanel;


    private MainGameState gameState;
    private Planet planet;

    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.planetForDetail;
        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);
        planetNameLabel.text = planet.name;
        planetShieldImg.enabled = false;
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
            ShipListItem2 shipListItem = newObj.GetComponent<ShipListItem2>();
            shipListItem.setShip(ship);
        }

        // Personnel
        planet.personnelsOnSurface.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Personnel personnel in planet.personnelsOnSurface)
        {
            Debug.Log(personnel.type.name);
            Transform panelTransform = personnel.team.Equals(Team.TeamA) ? personnelTeamAPanel : personnelTeamBPanel;
            newObj = (GameObject)Instantiate(personnelListItemPrefab, panelTransform);
            PersonnelListItem personnelListItem = newObj.GetComponent<PersonnelListItem>();
            personnelListItem.setPersonnel(personnel);
        }
    }

    private void clearAll()
    {
        clearPanel(shipsTeamAPanel);
        clearPanel(shipsTeamBPanel);
        clearPanel(personnelTeamAPanel);
        clearPanel(personnelTeamBPanel);
    }

    private void clearPanel(Transform panel)
    {
        foreach (Transform child in panel)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
