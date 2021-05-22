using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlanetDetailImage : DragAndDroppable
{
    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem" };
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        gameObject.GetComponent<Image>().color = Color.yellow;
    }

    protected override void onPointExit()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }

    protected override void onDrop(GameObject pointerDrag)
    {
        gameObject.GetComponent<Image>().color = Color.white;
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
