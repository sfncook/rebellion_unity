using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionReportDialog : MonoBehaviour
{
    public HeaderControls headerControls;
    public Image reporterImg;
    public TextMeshProUGUI reporterNameText;
    public Image planetImg;
    public TextMeshProUGUI planetNameText;
    public TextMeshProUGUI successText;
    public GameObject row1;
    public GameObject row2;
    public GameObject row3;
    public GameObject row4;
    public Button ackButton;

    private MissionReport report;

    void Start()
    {
        ackButton.onClick.AddListener(onClickAck);
        MainGameState.gameState.stopTimerEvent.Invoke();
        report = (MissionReport) MainGameState.gameState.reportForDialog;

        Planet planet = MainGameState.findPlanetForPersonnel(report.reporter);
        planetNameText.text = planet.name;
        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/" + planet.name);

        headerControls.setShowBackButton(!report.showImmediately);

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

        if(report.success)
        {
            successText.text = "Mission Success!";
            successText.color = Color.green;
        } else
        {
            successText.text = "Mission Failed!";
            successText.color = Color.red;
        }

        row1.SetActive(false);
        setRowText(row1, "Day Complete:", report.dayComplete.ToString());
        row2.SetActive(false);
        row3.SetActive(false);
        row4.SetActive(false);
        if (report is DiplomacyMissionReport)
        {
            headerControls.setHeaderTitle("Diplomacy Report");
            DiplomacyMissionReport diplomacyMissionReport = (DiplomacyMissionReport)report;
            if (report.success)
            {
                float loyaltyDeltaF = diplomacyMissionReport.loyaltyDelta;
                string loyaltyDeltaStr = ((int)(loyaltyDeltaF * 100)).ToString();
                setRowText(row2, "Loyalty Increase:", loyaltyDeltaStr + "%");
            } else
            {
                if(diplomacyMissionReport.loyaltyLost)
                {
                    float loyaltyLostDeltaF = diplomacyMissionReport.loyaltyLostDelta;
                    string loyaltyLostDeltaStr = ((int)(loyaltyLostDeltaF * 100)).ToString();
                    setRowText(row2, "Loyalty Lost:", loyaltyLostDeltaStr + "%");
                }
            }
        }
        else if (report is RecruiterMissionReport)
        {
            headerControls.setHeaderTitle("Recruiting Report");
            if (report.success)
            {
                Personnel recruitedPersonnel = ((RecruiterMissionReport)report).recruitedPersonnel;
                string recruitedName = "";
                if (recruitedPersonnel.isHero())
                {
                    recruitedName = recruitedPersonnel.hero.moniker;
                }
                else
                {
                    recruitedName = recruitedPersonnel.type.name;
                }
                setRowText(row2, "Recruited:", recruitedName);
            }
        }
        else if (report is EspionageMissionReport)
        {
            headerControls.setHeaderTitle("Espionage Mission Report");
            if (report.success)
            {
            }
        }
    }

    private void setRowText(GameObject row, string key, string value)
    {
        row.SetActive(true);

        Transform keyTrans = row.transform.Find("Key");
        keyTrans.gameObject.GetComponent<TextMeshProUGUI>().text = key;

        Transform valueTrans = row.transform.Find("Value");
        valueTrans.gameObject.GetComponent<TextMeshProUGUI>().text = value;
    }

    private void onClickAck()
    {
        MainGameState.gameState.ackReport(report);
        MainGameState.gameState.reportForDialog = null;
        SceneManager.LoadScene("Planet Detail 2");
    }

    public void onClickBackButton()
    {
        MainGameState.gameState.reportForDialog = null;
        if(MainGameState.gameState.planetForDetail!=null)
        {
            SceneManager.LoadScene("Planet Detail 2");
        } else if (MainGameState.gameState.sectorForDetail != null)
        {
            SceneManager.LoadScene("Sector Map 2");
        } else
        {
            SceneManager.LoadScene("Galaxy Map");
        }
    }
}
