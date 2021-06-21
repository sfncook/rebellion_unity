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
    }

    private void onClickCancel()
    {
        personnel = null;
        missionTargetUnit = null;
        missionTargetPlanet = null;
        gameObject.SetActive(false);
    }

    private void onClickAssign()
    {
        personnel.assignMission(selectedMissionType, missionTargetUnit, missionTargetPlanet);
        gameObject.SetActive(false);
        updatePlanetDetail();
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
            //personnelListItem.setPersonnel(personnel);
            //personnelListItem.setCanvas(canvas);
        }
    }

    private void clearGrid()
    {
        foreach (Transform child in missionTypesPanel)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
