using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ReportRow : MonoBehaviour
{
    public Image reportSeverityImg;
    public TextMeshProUGUI missionTypeNameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI planetName;
    public TextMeshProUGUI dayCompleteText;
    public Image planetImg;

    private Report report;
    private Planet planet;

    public void setReport(Report report)
    {
        this.report = report;

        string severityImgPath = "";
        Color severityColor = Color.white;
        switch(report.severity)
        {
            case ReportSeverity.Danger:
                severityImgPath = "Clean Vector Icons/T_1_fire_.png";
                severityColor = Color.red;
                break;
            case ReportSeverity.Failure:
                severityImgPath = "Clean Vector Icons/T_10_flag_destroyed_.png";
                severityColor = new Color(1.0f, 0.2f, 0.2f);
                break;
            case ReportSeverity.Info:
                severityImgPath = "Clean Vector Icons/T_16_message_.png";
                severityColor = Color.blue;
                break;
            case ReportSeverity.Success:
                severityImgPath = "40+ Simple Icons - Free/CheckMark_Simple_Icons_UI.png";
                severityColor = Color.green;
                break;
            case ReportSeverity.Warning:
                severityImgPath = "Clean Vector Icons/T_9_flag_.png";
                severityColor = Color.yellow;
                break;
        }
        reportSeverityImg.sprite = Resources.Load<Sprite>(severityImgPath);
        reportSeverityImg.color = severityColor;

        planet = MainGameState.findPlanetForPersonnel(report.reporter);
        planetName.text = planet.name;
        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);

        descriptionText.text = report.title;
        dayCompleteText.text = report.dayComplete.ToString();

        if(report is DiplomacyMissionReport)
        {
            missionTypeNameText.text = "Diplomacy";
        }
        else if (report is RecruiterMissionReport)
        {
            missionTypeNameText.text = "Recruitment";
        }
        else if (report is EspionageMissionReport)
        {
            missionTypeNameText.text = "Espionage";
        }
        else if (report is StoryLineReport)
        {
            missionTypeNameText.text = "Storyline";
        }
    }

    public void setIsAcked(bool isAcked)
    {
        Color color = new Color(1f, 1f, 1f, 0.3f);
        if (!isAcked)
        {
            color = new Color(0.5f, 0.5f, 1.0f, 0.3f);
        }
        gameObject.GetComponent<Image>().color = color;
    }

    //private void OnMouseUp()
    //{
    //    StarSector sector = MainGameState.findSectorForPlanet(planet);
    //    MainGameState.gameState.sectorForDetail = sector;
    //    MainGameState.gameState.planetForDetail = planet;
    //    SceneManager.LoadScene("Planet Detail 2");
    //}
}
