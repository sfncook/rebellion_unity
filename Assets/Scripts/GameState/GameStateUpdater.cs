using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameStateUpdater : MonoBehaviour
{
    private const float SEC_PER_GAMEDAY = 1.0f;

    public MainGameState mainGameState;
    public TextMeshProUGUI gameTimeLabel;
    public Button btnStartStop;

    private bool isTimerRunning = false;
    private float timerSec = 0.0f;
    private int gameDay = 0;
    private UnityEvent gameStateUpdateEvent = new UnityEvent();

    private void Update()
    {
        if(isTimerRunning)
        {
            if (timerSec <= 0.0f)
            {
                ++gameDay;
                string gameDayStr = gameDay.ToString();
                gameTimeLabel.text = "Day: " + gameDayStr.PadLeft(3, '0');
                timerSec = SEC_PER_GAMEDAY;

                Planet planet = mainGameState.getPlanetByName("Baphauhines");
                planet.loyalty += 5.0f;
                if(planet.loyalty >= 100)
                {
                    planet.loyalty = 0.0f;
                }
                gameStateUpdateEvent.Invoke();
            }
            else
            {
                timerSec -= Time.deltaTime;
            }
        }
    }

    public void onClickStartStop()
    {
        isTimerRunning = !isTimerRunning;
        if (isTimerRunning)
        {
            btnStartStop.GetComponentInChildren<TextMeshProUGUI>().text = "Stop";
        }
        else
        {
            btnStartStop.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        }
    }

    public void AddListenerGameStateUpdateEvent(UnityAction call)
    {
        gameStateUpdateEvent.AddListener(call);
    }
}
