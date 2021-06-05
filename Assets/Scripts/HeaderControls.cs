using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class OnClickBackButton : UnityEvent
{
}

public class HeaderControls : MonoBehaviour
{
    public Button backButton;
    public OnClickBackButton onClickBackButton;
    public TextMeshProUGUI headerTitleText;

    private void Start()
    {
        backButton.onClick.AddListener(onClickBack);
    }

    private void onClickBack()
    {
        MainGameState.gameState.sectorForDetail = null;
        SceneManager.LoadScene("Galaxy Map");
    }

    public void setHeaderTitle(string headerTitle)
    {
        headerTitleText.text = headerTitle;
    }

}
