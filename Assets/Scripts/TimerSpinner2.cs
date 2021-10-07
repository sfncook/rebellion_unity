using UnityEngine;

public class TimerSpinner2 : MonoBehaviour
{
    void FixedUpdate()
    {
        if (MainGameState.gameState.getIsTimerRunning())
        {
            transform.Rotate(Vector3.forward * -(Time.deltaTime * 180.0f));
            //transform.Rotate(Vector3.forward * -5);
        }
    }

    private void OnMouseUp()
    {
        if (MainGameState.gameState.getIsTimerRunning())
        {
            MainGameState.gameState.stopTimer();
        }
        else
        {
            MainGameState.gameState.startTimer();
        }
    }
}
