using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionTypeListItem : MonoBehaviour
{
    public Image missionTypeImg;

    public delegate void ClickMissionType(MissionType missionType);
    public ClickMissionType clickMissionType;

    private MissionType missionType;

    public void setMissionType(MissionType missionType)
    {
        this.missionType = missionType;
    }

    private void OnMouseUp()
    {
        clickMissionType(missionType);
    }
}
