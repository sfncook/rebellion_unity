using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DefenseListItem : DragAndDroppable
{
    public SpriteRenderer defenseImg;

    public void setDefense(Defense defense)
    {
        defenseImg.sprite = Resources.Load<Sprite>("Images/Defenses/" + defense.type.name);
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { };
    }

    //protected override void onPointEnter(GameObject pointerDrag)
    //{
    //    defenseImg.gameObject.GetComponent<Image>().color = Color.yellow;
    //}
    //protected override void onPointExit()
    //{
    //    defenseImg.gameObject.GetComponent<Image>().color = Color.white;
    //}

    //protected override void onDrop(GameObject pointerDrag)
    //{
    //    defenseImg.gameObject.GetComponent<Image>().color = Color.white;
    //}

    protected override bool isDraggable()
    {
        return false;
    }
    protected override bool isDroppable()
    {
        return false;
    }
}
