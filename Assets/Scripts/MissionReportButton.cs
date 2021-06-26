using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionReportButton : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        MainGameState.gameState.addListenerPostCleanupEvent(onPostPrepEvent);
    }

    private void onPostPrepEvent()
    {
        gameObject.SetActive(MainGameState.gameState.reportsUnAcked.Count > 0);
    }

    public void OnMouseUp()
    {
        SceneManager.LoadScene("Mission Report Dialog");
    }
}
