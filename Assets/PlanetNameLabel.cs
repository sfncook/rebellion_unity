using UnityEngine;
using TMPro;

public class PlanetNameLabel : MonoBehaviour
{
    private MainGameState gameState;
    private Planet selectedPlanet;

    void Start()
    {
        gameState = MainGameState.gameState;
        selectedPlanet = gameState.planetForDetail;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = selectedPlanet.name;
    }
}
