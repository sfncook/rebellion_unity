using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    void OnMouseDown()
    {
        MainGameState.gameState.planetForDetail = null;
        SceneManager.LoadScene("Sector Map 2");
    }
}
