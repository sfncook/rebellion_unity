using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : BladeFadeScene
{
    public Button startButton;
    
    void Start()
    {
        fadein = true;
        blackFade.color = Color.black;
        startButton.onClick.AddListener(onClickStart);
    }

    public void onClickStart()
    {
        fadeout = true;
    }

    protected override void fadeOutComplete()
    {
        SceneManager.LoadScene("Introduction Scene");
    }

}
