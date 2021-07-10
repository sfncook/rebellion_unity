using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoReportDialog : AbstractReportDialog
{
    public Image targetImg;
    public TextMeshProUGUI targetNameText;
    public TextMeshProUGUI contentText;

    void Start()
    {
        _Start();

        InfoReport infoReport = (InfoReport)report;
        contentText.text = infoReport.content;

        string targetImagePath = "";
        if (infoReport.targetUnit is Planet)
        {
            Planet targetPlanet = (Planet)infoReport.targetUnit;
            targetNameText.text = targetPlanet.name;
            targetImagePath = "Images/Planets/" + targetPlanet.name;
        } else if (infoReport.targetUnit is Personnel)
        {
            Personnel targetPersonnel = (Personnel)infoReport.targetUnit;
            if (targetPersonnel.isHero())
            {
                targetImagePath = "Images/Heros/" + targetPersonnel.hero.moniker;
                targetNameText.text = targetPersonnel.hero.moniker;
            }
            else
            {
                targetImagePath = "Images/Personnel/" + targetPersonnel.type.name;
                targetNameText.text = targetPersonnel.type.name;
            }
        }
        reporterImg.sprite = Resources.Load<Sprite>(targetImagePath);
    }

}
