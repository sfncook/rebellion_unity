using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public abstract class AbstractReportDialog: MonoBehaviour
{
    public HeaderControls headerControls;
    public Image reporterImg;
    public TextMeshProUGUI reporterNameText;
    public Button ackButton;
    public TextMeshProUGUI severityText;

    protected Report report;

    protected void _Start()
    {
        ackButton.onClick.AddListener(onClickAck);
        MainGameState.gameState.stopTimerEvent.Invoke();
        report = MainGameState.gameState.reportForDialog;

        MainGameState.gameState.ackReport(report);

        headerControls.setShowBackButton(!report.showImmediately);
        string headerTitle = "";
        if (report is DiplomacyMissionReport)
        {
            headerTitle = "Diplomacy Report";
        }
        else if (report is RecruiterMissionReport)
        {
            headerTitle = "Recruiting Report";
        }
        else if (report is EspionageMissionReport)
        {
            headerTitle = "Espionage Mission Report";
        }
        headerControls.setHeaderTitle(headerTitle);

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

        switch(report.severity)
        {
            case ReportSeverity.Danger:
                severityText.text = "ALERT!";
                break;
            case ReportSeverity.Failure:
                severityText.text = "Mission Failure!";
                break;
            case ReportSeverity.Info:
                severityText.text = "Notice";
                break;
            case ReportSeverity.Success:
                severityText.text = "Mission Success!";
                break;
            case ReportSeverity.Warning:
                severityText.text = "WARNING";
                break;
        }
        severityText.color = ReportSeverityHelper.severityToColor[report.severity];
    }

    private void closeDialog()
    {
        MainGameState.gameState.reportForDialog = null;
        if (MainGameState.gameState.planetForDetail != null)
        {
            SceneManager.LoadScene("Planet Detail 2");
        }
        else if (MainGameState.gameState.sectorForDetail != null)
        {
            SceneManager.LoadScene("Sector Map 2");
        }
        else
        {
            SceneManager.LoadScene("Galaxy Map");
        }
    }

    private void onClickAck()
    {
        closeDialog();
    }

    public void onClickBackButton()
    {
        closeDialog();
    }
}
