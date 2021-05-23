using UnityEngine;

public class TimerSpinner : MonoBehaviour
{
    void FixedUpdate()
    {
        if(MainGameState.gameState.getIsTimerRunning())
        {
            transform.Rotate(Vector3.forward * (Time.deltaTime * 180.0f));
        }
    }
}
