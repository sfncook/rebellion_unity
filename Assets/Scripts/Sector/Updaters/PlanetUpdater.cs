using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetUpdater : MonoBehaviour
{
    public SpriteRenderer imgFactory;
    public SpriteRenderer imgDefense;
    public SpriteRenderer imgConflict;
    public SpriteRenderer imgShipTeamA;
    public SpriteRenderer imgShipTeamB;
    public SpriteRenderer personnelTeamA;
    public SpriteRenderer personnelTeamB;

    private const float LOYALTY_BAR_TOTAL_WIDTH = 25.0f;
    private MainGameState gameState;
    private Planet planet;
    private Transform teamALoyaltyBar;


    // Start is called before the first frame update
    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.getPlanetByName(gameObject.name);
        gameState.addListenerUiUpdateEvent(onGameStateUpdate);
        teamALoyaltyBar = gameObject.transform.Find("Loyalty").Find("Offset").Find("TeamA");

        // Hide unused energy squares
        for (int i = planet.energyCapacity + 1; i <= 10; i++)
        {
            string strI = i.ToString().PadLeft(2, '0');
            Transform energySquare = gameObject.transform.Find("Resources").Find("Square" + strI);
            energySquare.gameObject.SetActive(false);
        }

        onGameStateUpdate();
    }

    void onGameStateUpdate()
    {
        float scaleX = (1.0f - planet.loyalty) * LOYALTY_BAR_TOTAL_WIDTH;
        float scaleY = teamALoyaltyBar.localScale.y;
        float scaleZ = teamALoyaltyBar.localScale.z;
        teamALoyaltyBar.localScale = new Vector3(scaleX, scaleY, scaleZ);

        float posX = scaleX / 2.0f;
        float posY = teamALoyaltyBar.localPosition.y;
        float posZ = teamALoyaltyBar.localPosition.z;
        teamALoyaltyBar.localPosition = new Vector3(posX, posY, posZ);

        Color loyaltyColor = Color.green;
        if (planet.loyalty > 0.5f)
        {
            loyaltyColor = Color.red;
        }
        imgFactory.color = loyaltyColor;
        imgDefense.color = loyaltyColor;
        imgConflict.color = loyaltyColor;

        bool planetHasShipsTeamA = planet.shipsInOrbit.Exists(ship => ship.team == Team.TeamA);
        bool planetHasShipsTeamB = planet.shipsInOrbit.Exists(ship => ship.team == Team.TeamB);
        imgShipTeamA.gameObject.SetActive(planetHasShipsTeamA);
        imgShipTeamB.gameObject.SetActive(planetHasShipsTeamB);

        bool planetHasPersonnelTeamA = planet.personnelsOnSurface.Exists(personnel => personnel.team == Team.TeamA);
        bool planetHasPersonnelTeamB = planet.personnelsOnSurface.Exists(personnel => personnel.team == Team.TeamB);
        personnelTeamA.gameObject.SetActive(planetHasPersonnelTeamA);
        personnelTeamB.gameObject.SetActive(planetHasPersonnelTeamB);

        imgFactory.gameObject.SetActive(planet.factories.Count > 0);
        imgConflict.gameObject.SetActive(planet.isInConflict);
        imgDefense.gameObject.SetActive(planet.defenses.Count > 0);

        int manyFacilities = planet.factories.Count + planet.defenses.Count;
        for (int i = 1; i<= manyFacilities; i++)
        {
            string strI = i.ToString().PadLeft(2, '0');
            Transform energySquare = gameObject.transform.Find("Resources").Find("Square" + strI);
            energySquare.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        for(int i=manyFacilities+1; i<=planet.energyCapacity; i++)
        {
            string strI = i.ToString().PadLeft(2, '0');
            Transform energySquare = gameObject.transform.Find("Resources").Find("Square" + strI);
            energySquare.GetComponent<SpriteRenderer>().color = Color.white;
        }

    }

    private void OnDestroy()
    {
        gameState.removeListenerGameStateUpdateEvent(onGameStateUpdate);
    }

    void OnMouseDown()
    {
        gameState.planetForDetail = planet;
        SceneManager.LoadScene("Planet2");
    }
}
