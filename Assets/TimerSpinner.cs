using UnityEngine;

public class TimerSpinner : MonoBehaviour
{
    private float rotateSpeed = 10.0f;

    private MainGameState gameState;

    private void Start()
    {
        Debug.Log("Start");
        gameState = MainGameState.gameState;
    }

    void FixedUpdate()
    {
        if(gameState.getIsTimerRunning())
        {
            transform.Rotate(Vector3.forward * (Time.deltaTime * 180.0f));
        }
    }
}
