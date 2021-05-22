using UnityEngine;

public class StarChart : MonoBehaviour
{
    public void startMoveShip()
    {
        Debug.Log("StarChart startMoveShip");
        gameObject.SetActive(true);
    }

    public void stopMoveShip()
    {
        gameObject.SetActive(false);
    }
}
