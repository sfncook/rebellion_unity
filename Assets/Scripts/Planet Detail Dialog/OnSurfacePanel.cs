using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OnSurfacePanel : DragAndDroppable
{
    public GameObject blankPersonnelPrefab;
    public Transform personnelTeamAPanel;
    public Transform personnelTeamBPanel;
    public Image planetDetailImage;
    public GameObject personnelListItemPrefab;
    public ShipContentsAndMovePanel shipContentsAndMovePanel;

    private Planet planet;
    private GameObject blankPersonnelDragOver;

    public delegate void UpdateShipGrids();
    public UpdateShipGrids updateShipGrids;

    public void setPlanet(Planet planet)
    {
        this.planet = planet;
        updateGrid();
    }

    public void setUpdateShipGrids(UpdateShipGrids updateShipGrids)
    {
        this.updateShipGrids = updateShipGrids;
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem2" };
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        PersonnelListItem2 personnelListItem = pointerDrag.GetComponent<PersonnelListItem2>();
        Personnel personnel = personnelListItem.getPersonnel();
        PersonnelType personnelType = (PersonnelType) personnel.type;
        if (personnelListItem.isLocatedOnShip())
        {
            planetDetailImage.color = Color.yellow;
            blankPersonnelDragOver = (GameObject)Instantiate(blankPersonnelPrefab, personnelTeamAPanel);
        } else  if(
            personnelType.availableMissionTypes.Contains(MissionType.espionage) ||
            personnelType.availableMissionTypes.Contains(MissionType.recruiting) ||
            personnelType.availableMissionTypes.Contains(MissionType.diplomacy)
            )
        {
            planetDetailImage.color = Color.blue;
        }
    }
    protected override void onPointExit()
    {
        planetDetailImage.color = Color.white;
        Destroy(blankPersonnelDragOver, 0.01f);
    }
    protected override void onDrop(GameObject pointerDrag)
    {
        planetDetailImage.color = Color.white;
        Destroy(blankPersonnelDragOver, 0.01f);
        PersonnelListItem2 personnelListItem = pointerDrag.GetComponent<PersonnelListItem2>();
        Personnel personnel = personnelListItem.getPersonnel();
        PersonnelType personnelType = (PersonnelType)personnel.type;
        if (personnelListItem.isLocatedOnShip())
        {
            personnelListItem.setOnShip(null);
            planet.personnelsOnSurface.Add(personnel);
            updateGrid();
            shipContentsAndMovePanel.removePersonnel(personnel);
            shipContentsAndMovePanel.updateGrid();
            updateShipGrids();
        } else if (
         personnelType.availableMissionTypes.Contains(MissionType.espionage) ||
         personnelType.availableMissionTypes.Contains(MissionType.recruiting) ||
         personnelType.availableMissionTypes.Contains(MissionType.diplomacy)
         )
        {
            MainGameState.gameState.showMissionAssignmentDialog.Invoke(personnel, null, planet);
        }
    }

    protected override bool isDraggable()
    {
        return false;
    }
    protected override bool isDroppable()
    {
        return true;
    }

    private void updateGrid()
    {
        clearAll();

        GameObject newObj;

        planet.personnelsOnSurface.Sort((a, b) => a.type.name.CompareTo(b.type.name));
        foreach (Personnel personnel in planet.personnelsOnSurface)
        {
            Transform panelTransform = personnel.team.Equals(Team.TeamA) ? personnelTeamAPanel : personnelTeamBPanel;
            newObj = (GameObject)Instantiate(personnelListItemPrefab, panelTransform);
            PersonnelListItem2 personnelListItem = newObj.GetComponent<PersonnelListItem2>();
            personnelListItem.setPersonnel(personnel);
            personnelListItem.setCanvas(canvas);
        }
    }

    private void clearAll()
    {
        clearPanel(personnelTeamAPanel);
        clearPanel(personnelTeamBPanel);
    }

    private void clearPanel(Transform panel)
    {
        foreach (Transform child in panel)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
