using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryLineReportDialog : MonoBehaviour
{
    public HeaderControls headerControls;
    public Image reporterImg;
    public TextMeshProUGUI reporterNameText;
    public TextMeshProUGUI contentText;
    public Button nextButton;
    public Button prevButton;

    private StoryLineReport report;
    private int curContentPage = 0;

    void Start()
    {
        report = (StoryLineReport) MainGameState.gameState.reportForDialog;
        headerControls.setHeaderTitle(report.title);
        nextButton.onClick.AddListener(onClickNext);
        prevButton.onClick.AddListener(onClickPrev);

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
        } else
        {
            nextButton.gameObject.SetActive(true);
        }

        contentText.text = report.contentPages[curContentPage];
    }
}
