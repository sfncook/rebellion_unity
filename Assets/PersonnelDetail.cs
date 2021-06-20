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

        visibilityBars.setValue(personnel.visibility);
    }

    public void onClickBackButton()
    {
        MainGameState.gameState.personnelForDetail = null;
        SceneManager.LoadScene("Planet Detail 2");
    }
}
