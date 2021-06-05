using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class OnClickBackButton : UnityEvent
{
}

public class HeaderControls : MonoBehaviour
{
    public Button backButton;
    public OnClickBackButton onClickBackButton;

    private void Start()
    {
        backButton.onClick.AddListener(onClickBack);
    }

    private void onClickBack()
    {
        MainGameState.gameState.sectorForDetail = null;
        SceneManager.LoadScene("Galaxy Map");
    }

}
