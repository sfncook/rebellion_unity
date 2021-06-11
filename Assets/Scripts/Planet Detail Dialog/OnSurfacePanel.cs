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
        return new List<string>() { "PersonnelListItem" };
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        PersonnelListItem personnelListItem = pointerDrag.GetComponent<PersonnelListItem>();
        if(personnelListItem.getLocatedOnShip())
        {
            planetDetailImage.color = Color.yellow;
            blankPersonnelDragOver = (GameObject)Instantiate(blankPersonnelPrefab, personnelTeamAPanel);
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
        PersonnelListItem personnelListItem = pointerDrag.GetComponent<PersonnelListItem>();
        if (personnelListItem.getLocatedOnShip())
        {
            Personnel personnel = personnelListItem.getPersonnel();
            planet.personnelsOnSurface.Add(personnel);
            updateGrid();
            //shipContentsAndMovePanel.removePersonnel(personnel);
            //shipContentsAndMovePanel.updateGrid();
            updateShipGrids();
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
            PersonnelListItem personnelListItem = newObj.GetComponent<PersonnelListItem>();
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
