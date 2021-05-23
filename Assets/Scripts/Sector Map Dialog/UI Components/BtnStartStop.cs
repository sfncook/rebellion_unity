using UnityEngine;
using TMPro;

public class BtnStartStop : MonoBehaviour
{
    private MainGameState gameState;

    void Start()
    {
        gameState = MainGameState.gameState;
        gameState.startTimerEvent.AddListener(onStartTimer);
        gameState.stopTimerEvent.AddListener(onStopTimer);
    }

    public void onClick()
    {
        if (gameState.getIsTimerRunning())
        {
            gameState.stopTimerEvent.Invoke();
        }
        else
        {
            gameState.startTimerEvent.Invoke();
        }
    }

    public void onStartTimer()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Stop";
    }

    public void onStopTimer()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
    }
}
