using UnityEngine;
using TMPro;

public class BtnStartStop : MonoBehaviour
{
    private MainGameState gameState;

    void Start()
    {
        gameState = MainGameState.gameState;
    }

    public void onClick()
    {
        gameState.isTimerRunning = !gameState.isTimerRunning;
        if (gameState.isTimerRunning)
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Stop";
        }
        else
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        }
    }
}
