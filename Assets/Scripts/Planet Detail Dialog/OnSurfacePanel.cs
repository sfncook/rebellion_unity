using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OnSurfacePanel : DragAndDroppable
{
    public GameObject blankPersonnelPrefab;
    public Transform personnelTeamAPanel;
    public Image planetDetailImage;

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem" };
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        
    }
    protected override void onPointExit()
    {
        Debug.Log("OnSurfacePanel onPointExit");
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
