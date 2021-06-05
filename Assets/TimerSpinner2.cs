using UnityEngine;

public class TimerSpinner2 : MonoBehaviour
{
    void FixedUpdate()
    {
        if (MainGameState.gameState.getIsTimerRunning())
        {
            transform.Rotate(Vector3.forward * (Time.deltaTime * 180.0f));
        }
    }

    private void OnMouseUp()
    {
        if (MainGameState.gameState.getIsTimerRunning())
        {
            MainGameState.gameState.stopTimerEvent.Invoke();
        }
        else
        {
            MainGameState.gameState.startTimerEvent.Invoke();
        }
    }
}
