using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public Button startButton;
    public Image blackFade;

    private bool fadein = false;
    private bool fadeout = false;
    private const float FADE_DUR_SEC = 2;
    
    void Start()
    {
        startButton.onClick.AddListener(onClickStart);
        fadein = true;
    }

    public void onClickStart()
    {
        Debug.Log("onClickStart");
        fadeout = true;
    }

    private void FixedUpdate()
    {
        if (fadein || fadeout)
        {
            float alpha = blackFade.color.a;
            if(fadein && alpha<=0)
            {
                fadein = false;
            } else if(fadeout && alpha>=1)
            {
                fadeout = false;
                SceneManager.LoadScene("Introduction Scene");
            }
            else
            {
                if(fadein)
                {
                    alpha -= (Time.deltaTime / FADE_DUR_SEC);
                } else
                {
                    alpha += (Time.deltaTime / FADE_DUR_SEC);
                }
                Debug.Log("alpha:"+alpha);
                blackFade.color = new Color(0, 0, 0, alpha);
            }
        }
    }
}
