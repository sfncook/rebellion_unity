using UnityEngine;
using TMPro;

public class PlanetDetail : MonoBehaviour
{
    public TextMeshProUGUI planetNameLabel;
    public SpriteRenderer planetImg;
    public SpriteRenderer planetShieldImg;
    public GameObject shipListItemPrefab;
    public Transform shipsTeamAPanel;
    public Transform shipsTeamBPanel;


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
    }

    private void clearAll()
    {
        foreach (Transform child in shipsTeamAPanel)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in shipsTeamBPanel)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
