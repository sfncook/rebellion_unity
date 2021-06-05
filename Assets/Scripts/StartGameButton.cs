using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public void onClick()
    {
        MainGameState.gameState.loadGameFromFiles();
        SceneManager.LoadScene("Galaxy Map");
    }
}
