using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionTypeListItem : MonoBehaviour
{
    public Image missionTypeImg;
    public Image bgColor;
    public TextMeshProUGUI missionTypeNameText;
    public TextMeshProUGUI percentText;

    public delegate void ClickMissionType(MissionType missionType);
    public ClickMissionType clickMissionType;

    private MissionType missionType;

    public void setMissionType(MissionType missionType, Personnel personnel)
    {
        this.missionType = missionType;
        missionTypeImg.sprite = Resources.Load<Sprite>("Images/Missions/" + missionType.name);
        missionTypeNameText.text = missionType.name;
        float percent = 0;
        Color color = Color.white;
        if(missionType == MissionType.espionage)
        {
            percent = personnel.espionage;
        } else if (missionType == MissionType.diplomacy)
        {
            percent = personnel.diplomacy;
        } else if (missionType == MissionType.recruiting)
        {
            percent = personnel.recruiting;
        }
        if(percent>=75)
        {
            color = Color.green;
        } else if (percent <= 15)
        {
            color = Color.red;
        } else if (percent <= 50)
        {
            color = Color.yellow;
        } else
        {
            color = Color.white;
        }
        percentText.text = ((int)percent).ToString() + "%";
        percentText.color = color;
    }

    private void OnMouseUp()
    {
        clickMissionType(missionType);
    }

    public void setIsSelected(bool isSelected)
    {
        bgColor.color = isSelected ? Color.yellow : Color.white;
    }
}
