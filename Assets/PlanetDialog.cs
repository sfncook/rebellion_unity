using UnityEngine;

public class PlanetDialog : MonoBehaviour
{
    private MainGameState gameState;
    private Planet selectedPlanet;

    void Start()
    {
        gameState = MainGameState.gameState;
        selectedPlanet = gameState.planetForDetail;
    }
}
