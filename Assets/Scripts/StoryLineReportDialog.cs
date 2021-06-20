using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryLineReportDialog : MonoBehaviour
{
    public HeaderControls headerControls;
    public Image reporterImg;
    public TextMeshProUGUI reporterNameText;
    public TextMeshProUGUI contentText;
    public Button nextButton;
    public Button prevButton;
    public Button doneButton;

    private StoryLineReport report;
    private int curContentPage = 0;

    void Start()
    {
        MainGameState.gameState.stopTimerEvent.Invoke();
        report = (StoryLineReport) MainGameState.gameState.reportForDialog;
        headerControls.setHeaderTitle(report.title);
        nextButton.onClick.AddListener(onClickNext);
        prevButton.onClick.AddListener(onClickPrev);
        doneButton.onClick.AddListener(onClickDone);

        string imagePath;
        if (report.reporter.isHero())
        {
            imagePath = "Images/Heros/" + report.reporter.hero.moniker;
            reporterNameText.text = report.reporter.hero.moniker;
        }
        else
        {
            imagePath = "Images/Personnel/" + report.reporter.type.name;
            reporterNameText.text = report.reporter.type.name;
        }
        reporterImg.sprite = Resources.Load<Sprite>(imagePath);

        curContentPage = 0;
        updateContents();
    }

    private void onClickNext()
    {
        curContentPage++;
        curContentPage = Mathf.Min(curContentPage, report.contentPages.Count-1);
        updateContents();
    }

    private void onClickPrev()
    {
        curContentPage--;
        curContentPage = Mathf.Max(curContentPage, 0);
        updateContents();
    }

    private void updateContents()
    {
        if(curContentPage==0)
        {
            prevButton.gameObject.SetActive(false);
        } else
        {
            prevButton.gameObject.SetActive(true);
        }
        if(curContentPage==report.contentPages.Count-1)
        {
            nextButton.gameObject.SetActive(false);
            doneButton.gameObject.SetActive(true);
        } else
        {
            nextButton.gameObject.SetActive(true);
            doneButton.gameObject.SetActive(false);
        }

        contentText.text = report.contentPages[curContentPage];
    }

    private void onClickDone()
    {
        MainGameState.gameState.reportForDialog = null;
        SceneManager.LoadScene("Planet Detail 2");
    }

    public void onClickBackButton()
    {
        MainGameState.gameState.reportForDialog = null;
        SceneManager.LoadScene("Planet Detail 2");
    }

}
