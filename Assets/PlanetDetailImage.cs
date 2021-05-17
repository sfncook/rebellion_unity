using UnityEngine;
using System.Collections.Generic;

public class PlanetDetailImage : DragAndDroppable
{
    public SpriteRenderer hoverImg;


    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem" };
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        Debug.Log("onPointEnter");
        hoverImg.gameObject.SetActive(true);
    }

    protected override void onPointExit()
    {
        Debug.Log("onPointExit");
        hoverImg.gameObject.SetActive(false);
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
