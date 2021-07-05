using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionAssignmentDialog : MonoBehaviour
{
    public Image personnelImg;
    public TextMeshProUGUI personnelNameText;
    public TextMeshProUGUI targetNameText;
    public Button cancelButton;
    public Button assignButton;
    public Transform missionTypesPanel;
    public GameObject missionTypesPrefab;

    public delegate void UpdatePlanetDetail();
    public UpdatePlanetDetail updatePlanetDetail;

    private Personnel personnel;
    private AbstractUnit missionTargetUnit;
    private Planet missionTargetPlanet;
    private MissionType selectedMissionType;

    private void Start()
    {
        cancelButton.onClick.AddListener(onClickCancel);
        assignButton.onClick.AddListener(onClickAssign);
        assignButton.gameObject.SetActive(false);
    }

    public void setMissionParameters(
        Personnel personnel,
        AbstractUnit missionTargetUnit,
        Planet missionTargetPlanet
    )
    {
        string imagePath;
        string name;
        if (personnel.isHero())
        {
            imagePath = "Images/Heros/" + personnel.hero.moniker;
            name = personnel.hero.moniker;
        }
        else
        {
            imagePath = "Images/Personnel/" + personnel.type.name;
            name = personnel.type.name;
        }
        personnelNameText.text = name;
        personnelImg.sprite = Resources.Load<Sprite>(imagePath);

        targetNameText.text = (missionTargetUnit != null) ? missionTargetUnit.type.name : missionTargetPlanet.name;

        this.personnel = personnel;
        this.missionTargetUnit = missionTargetUnit;
        this.missionTargetPlanet = missionTargetPlanet;

        updateGrid();
    }

    private void onClickCancel()
    {
        personnel = null;
        missionTargetUnit = null;
        missionTargetPlanet = null;
        gameObject.SetActive(false);
        selectedMissionType = null;
        assignButton.gameObject.SetActive(false);
    }

    private void onClickAssign()
    {
        personnel.assignMission(selectedMissionType, missionTargetUnit, missionTargetPlanet);
        gameObject.SetActive(false);
        updatePlanetDetail();
        selectedMissionType = null;
        assignButton.gameObject.SetActive(false);
    }

    private void updateGrid()
    {
        clearGrid();

        GameObject newObj;
        PersonnelType type = (PersonnelType)personnel.type;
        foreach (MissionType missionType in type.availableMissionTypes)
        {
            newObj = (GameObject)Instantiate(missionTypesPrefab, missionTypesPanel);
            MissionTypeListItem missionTypeListItem = newObj.GetComponent<MissionTypeListItem>();
            missionTypeListItem.setMissionType(missionType);
            missionTypeListItem.clickMissionType = setSelectedMissionType;
            missionTypeListItem.setIsSelected(missionType.Equals(selectedMissionType));
        }
    }

    private void clearGrid()
    {
        foreach (Transform child in missionTypesPanel)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void setSelectedMissionType(MissionType missionType)
    {
        selectedMissionType = missionType;
        updateGrid();
        assignButton.gameObject.SetActive(true);
    }

    public void setUpdatePlanetDetail(UpdatePlanetDetail updatePlanetDetail)
    {
        this.updatePlanetDetail = updatePlanetDetail;
    }
}
