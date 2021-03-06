using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlanetUpdater : MonoBehaviour
{
    private readonly Dictionary<Team, int> teamToShipCount = new Dictionary<Team, int>
    {
        {Team.TeamA, 0},
        {Team.TeamB, 0}
    };

    public SpriteRenderer imgFactory;
    public SpriteRenderer imgDefense;
    public SpriteRenderer imgConflict;
    public SpriteRenderer imgShipTeamA;
    public SpriteRenderer imgShipTeamB;
    public SpriteRenderer personnelTeamA;
    public SpriteRenderer personnelTeamB;
    public SpriteRenderer shieldImg;
    public LoyaltyBars loyaltyBars;

    private MainGameState gameState;
    private Planet planet;


    // Start is called before the first frame update
    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.getPlanetByName(gameObject.name);
        gameState.addListenerUiUpdateEvent(onUiUpdateEvent);

        // Hide unused energy squares
        for (int i = planet.energyCapacity + 1; i <= 10; i++)
        {
            string strI = i.ToString().PadLeft(2, '0');
            Transform energySquare = gameObject.transform.Find("Resources").Find("Square" + strI);
            energySquare.gameObject.SetActive(false);
        }

        bool hasOrbitalShield = planet.defenses.Exists(defense => defense.type.Equals(DefenseType.planetaryShield));
        shieldImg.enabled = hasOrbitalShield;

        onUiUpdateEvent();
    }

    void onUiUpdateEvent()
    {
        loyaltyBars.setPlanet(planet);

        Color loyaltyColor = planet.getTeam().getColorForTeam();
        imgFactory.color = loyaltyColor;
        imgDefense.color = loyaltyColor;

        bool planetHasShipsTeamA = planet.shipsInOrbit.Exists(ship => ship.team == Team.TeamA);
        bool planetHasShipsTeamB = planet.shipsInOrbit.Exists(ship => ship.team == Team.TeamB);
        imgShipTeamA.gameObject.SetActive(planetHasShipsTeamA);
        imgShipTeamB.gameObject.SetActive(planetHasShipsTeamB);

        bool planetHasPersonnelTeamA = planet.personnelsOnSurface.Exists(personnel => personnel.team == Team.TeamA);
        bool planetHasPersonnelTeamB = planet.personnelsOnSurface.Exists(personnel => personnel.team == Team.TeamB);
        personnelTeamA.gameObject.SetActive(planetHasPersonnelTeamA);
        personnelTeamB.gameObject.SetActive(planetHasPersonnelTeamB);

        imgFactory.gameObject.SetActive(planet.factories.Count > 0);
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

        // conflict
        planet.isInConflict = false;

        foreach (Ship ship in planet.shipsInOrbit)
        {
            teamToShipCount[ship.team]++;
        }
        planet.isInConflict = (teamToShipCount[Team.TeamA] > 0) && (teamToShipCount[Team.TeamB] > 0);
        imgConflict.gameObject.SetActive(planet.isInConflict);

    }

    private void OnDestroy()
    {
        gameState.removeListenerGameStateUpdateEvent(onUiUpdateEvent);
    }

    void OnMouseDown()
    {
        gameState.planetForDetail = planet;
        gameState.stopTimerEvent.Invoke();
        SceneManager.LoadScene("Planet Detail");
    }
}
