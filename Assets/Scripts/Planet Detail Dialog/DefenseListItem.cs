using UnityEngine;
using System.Collections.Generic;

public class DefenseListItem : DragAndDroppable
{
    public SpriteRenderer defenseImg;

    public void setDefense(Defense defense)
    {
        defenseImg.sprite = Resources.Load<Sprite>("Images/Defenses/" + defense.type.name);
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem" };
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        Debug.Log("DefenseListItem onPointEnter");
    }
    protected override void onPointExit()
    {
        Debug.Log("DefenseListItem onPointExit");
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
