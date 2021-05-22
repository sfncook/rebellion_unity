using UnityEngine;
using System.Collections.Generic;

public class OnSurfacePanel : DragAndDroppable
{
    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem" };
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        Debug.Log("OnSurfacePanel onPointEnter");
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
