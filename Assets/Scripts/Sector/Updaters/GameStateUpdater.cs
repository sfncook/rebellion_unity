using UnityEngine;

public class GameStateUpdater : MonoBehaviour
{
    private const float SEC_PER_GAMEDAY = 1.0f;

    private MainGameState gameState;
    private float timerSec = 0.0f;

    private void Start()
    {
        gameState = MainGameState.gameState;
    }

    private void Update()
    {
        if(gameState.isTimerRunning)
        {
            if (timerSec <= 0.0f)
            {
                ++gameState.gameTime;
                timerSec = SEC_PER_GAMEDAY;

                Planet planet = gameState.getPlanetByName("Ater");
                planet.loyalty += 0.05f;
                if (planet.loyalty >= 1.0)
                {
                    planet.loyalty = 0.0f;
                }

                gameState.invokeAgentPlanEvent();
                gameState.invokeAgentActionEvent();
                gameState.invokePostCleanupEvent();
                gameState.invokeUiUpdateEvent();
            }
            else
            {
                timerSec -= Time.deltaTime;
            }
        } else
        {
            // Reset timer
            timerSec = SEC_PER_GAMEDAY;
        }
    }
}
