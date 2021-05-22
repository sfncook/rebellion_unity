using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OnSurfacePanel : DragAndDroppable
{
    public GameObject blankPersonnelPrefab;
    public Transform personnelTeamAPanel;
    public Image planetDetailImage;

    private Planet planet;
    private GameObject blankPersonnelDragOver;

    public void setPlanet(Planet planet)
    {
        this.planet = planet;
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem" };
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        planetDetailImage.color = Color.yellow;
        blankPersonnelDragOver = (GameObject)Instantiate(blankPersonnelPrefab, personnelTeamAPanel);
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
        Personnel personnel = personnelListItem.getPersonnel();
        foreach (Ship ship in planet.shipsInOrbit)
        {
            ship.personnelsOnBoard.Remove(personnel);
        }
        planet.personnelsOnSurface.Add(personnel);
    }

    protected override bool isDraggable()
    {
        return false;
    }
    protected override bool isDroppable()
    {
        return true;
    }

}
