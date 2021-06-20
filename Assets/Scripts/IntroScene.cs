using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScene : BladeFadeScene
{
    public List<GameObject> pagePanels;
    public Button skipButton;
    public Button nextPageButton;

    private int curPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        fadein = true;
        blackFade.color = Color.black;
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
        fadeout = true;
    }

    protected override void fadeOutComplete()
    {
        MainGameState.gameState.initializeNewGame();
    }
}
