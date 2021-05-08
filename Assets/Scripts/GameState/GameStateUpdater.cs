using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStateUpdater : MonoBehaviour
{
    private const float SEC_PER_GAMEDAY = 1.0f;

    public MainGameState gameState;
    public TextMeshProUGUI gameTimeLabel;
    public Button btnStartStop;

    private float timerSec = 0.0f;

    private void updateGameState()
    {
        if(gameState.isTimerRunning)
        {
            if (timerSec <= 0.0f)
            {
                ++gameState.gameTime;
                string gameDayStr = gameState.gameTime.ToString();
                gameTimeLabel.text = "Day: " + gameDayStr.PadLeft(3, '0');
                timerSec = SEC_PER_GAMEDAY;

                //Planet planet = mainGameState.getPlanetByName("Baphauhines");
                //planet.loyalty += 5.0f;
                //if(planet.loyalty >= 100)
                //{
                //    planet.loyalty = 0.0f;
                //}

                gameState.gameStateUpdateEvent.Invoke();
            }
            else
            {
                timerSec -= Time.deltaTime;
            }
        }
    }

    public void onClickStartStop()
    {
        gameState.isTimerRunning = !gameState.isTimerRunning;
        if (gameState.isTimerRunning)
        {
            btnStartStop.GetComponentInChildren<TextMeshProUGUI>().text = "Stop";
        }
        else
        {
            timerSec = SEC_PER_GAMEDAY;
            btnStartStop.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        }
    }
}
