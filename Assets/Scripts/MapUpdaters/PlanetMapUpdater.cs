using UnityEngine;

public class PlanetMapUpdater : MonoBehaviour
{
    private MainGameState gameState;
    private Planet planet;

    // Start is called before the first frame update
    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.getPlanetByName(gameObject.name);
    }
}
