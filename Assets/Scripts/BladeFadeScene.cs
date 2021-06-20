using UnityEngine;
using UnityEngine.UI;

public abstract class BladeFadeScene : MonoBehaviour
{
    public Image blackFade;

    protected bool fadein = false;
    protected bool fadeout = false;
    private const float FADE_DUR_SEC = 2;

    void Start()
    {
        blackFade.color = Color.black;
    }

    protected virtual void fadeInComplete()
    {
    }

    protected virtual void fadeOutComplete()
    {
    }

    protected void _FixedUpdate()
    {
        FixedUpdate();
    }

    private void FixedUpdate()
    {
        if (fadein || fadeout)
        {
            float alpha = blackFade.color.a;
            if (fadein && alpha <= 0)
            {
                fadein = false;
                fadeInComplete();
            }
            else if (fadeout && alpha >= 1)
            {
                fadeout = false;
                fadeOutComplete();
            }
            else
            {
                if (fadein)
                {
                    alpha -= (Time.deltaTime / FADE_DUR_SEC);
                }
                else
                {
                    alpha += (Time.deltaTime / FADE_DUR_SEC);
                }
                blackFade.color = new Color(0, 0, 0, alpha);
            }
        }
    }

}
