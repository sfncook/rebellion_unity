using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionTypeListItem : MonoBehaviour
{
    public Image missionTypeImg;
    public Image bgColor;
    public TextMeshProUGUI missionTypeNameText;

    public delegate void ClickMissionType(MissionType missionType);
    public ClickMissionType clickMissionType;

    private MissionType missionType;

    public void setMissionType(MissionType missionType)
    {
        this.missionType = missionType;
        missionTypeImg.sprite = Resources.Load<Sprite>("Images/Missions/" + missionType.name);
        missionTypeNameText.text = missionType.name;
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
