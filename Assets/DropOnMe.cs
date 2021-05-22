using System.Collections.Generic;
using UnityEngine;

public class DropOnMe : DragAndDroppable
{
    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem" };
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        Debug.Log("onPointEnter");
    }

    protected override void onPointExit()
    {
        Debug.Log("onPointExit");
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
