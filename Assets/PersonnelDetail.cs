using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PersonnelDetail : MonoBehaviour
{
    public HeaderControls headerControls;
    public Image personnelImg;
    public TextMeshProUGUI recruitingText;
    public TextMeshProUGUI diplomacyText;
    public TextMeshProUGUI espionageText;
    public ValueBars visibilityBars;
    public GameObject missionPanel;
    public TextMeshProUGUI missionTypeText;
    public TextMeshProUGUI missionTargetNameText;
    public TextMeshProUGUI missionCompleteText;

    private Personnel personnel;

    void Start()
    {
        personnel = MainGameState.gameState.personnelForDetail;

        string imagePath;
        string name;
        if (personnel.isHero())
        {
            imagePath = "Images/Heros/" + personnel.hero.moniker;
            name = personnel.hero.moniker;
        } else
        {
            imagePath = "Images/Personnel/" + personnel.type.name;
            name = personnel.type.name;
        }
        headerControls.setHeaderTitle(name);
        personnelImg.sprite = Resources.Load<Sprite>(imagePath);

        recruitingText.text = personnel.recruiting.ToString();
        diplomacyText.text = personnel.diplomacy.ToString();
        espionageText.text = personnel.espionage.ToString();

        visibilityBars.setValue(personnel.visibility/100f);

        if(personnel.hasMission())
        {
            missionPanel.SetActive(true);
            missionTypeText.text = personnel.activeMission.name;
            missionTargetNameText.text = (personnel.missionTargetUnit!=null) ? personnel.missionTargetUnit.type.name: personnel.missionTargetPlanet.name;
            missionCompleteText.text = personnel.dayMissionComplete.ToString();
        } else
        {
            missionPanel.SetActive(false);
        }
    }

    public void onClickBackButton()
    {
        MainGameState.gameState.personnelForDetail = null;
        SceneManager.LoadScene("Planet Detail 2");
    }
}
