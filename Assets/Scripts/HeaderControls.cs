using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
        onClickBackButton.Invoke();
    }

    public void setHeaderTitle(string headerTitle)
    {
        headerTitleText.text = headerTitle;
    }

    public void setShowBackButton(bool showBackButton)
    {
        backButton.gameObject.SetActive(showBackButton);
    }

}
