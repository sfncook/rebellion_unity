using UnityEngine;
using TMPro;

public class TextGameTime : MonoBehaviour
{
    private MainGameState gameState;

    private void Start()
    {
        gameState = MainGameState.gameState;
        gameState.addListenerGameStateUpdateEvent(onGameStateUpdate);
    }

    void onGameStateUpdate()
    {
        string gameDayStr = gameState.gameTime.ToString();
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Day: " + gameDayStr.PadLeft(3, '0');
    }

    private void OnDestroy()
    {
        gameState.removeListenerGameStateUpdateEvent(onGameStateUpdate);
    }
}
