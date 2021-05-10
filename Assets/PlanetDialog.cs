using UnityEngine;

public class PlanetDialog : MonoBehaviour
{
    private MainGameState gameState;
    private Planet selectedPlanet;
    public TabType selectedTab = TabType.Personnel;

    void Start()
    {
        gameState = MainGameState.gameState;
        selectedPlanet = gameState.planetForDetail;
    }
}
