using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    public List<GameObject> pagePanels;
    public Button skipButton;
    public Button nextPageButton;
    public Image blackFade;

    private int curPage = 0;

    private bool fading = false;
    private const float FADE_DUR_SEC = 2;

    // Start is called before the first frame update
    void Start()
    {
        skipButton.onClick.AddListener(onClickSkip);
        nextPageButton.onClick.AddListener(onClickNextPage);
        updatePage();
    }

    private void updatePage()
    {
        foreach(GameObject page in pagePanels)
        {
            page.SetActive(false);
        }
        pagePanels[curPage].SetActive(true);
    }

    private void onClickNextPage()
    {
        curPage++;
        if(curPage<pagePanels.Count)
        {
            updatePage();
        } else
        {
            loadNextScene();
        }
    }

    private void onClickSkip()
    {
        loadNextScene();
    }

    private void loadNextScene()
    {
        fading = true;
    }

    private void FixedUpdate()
    {
        if(fading)
        {
            float alpha = blackFade.color.a;
            if(alpha>=1)
            {
                fading = false;
                MainGameState.gameState.initializeNewGame();
            } else
            {
                alpha += (Time.deltaTime/FADE_DUR_SEC);
                blackFade.color = new Color(0, 0, 0, alpha);
            }
        }
    }
}
