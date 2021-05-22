using UnityEngine;
using System.Collections.Generic;

public class FactoryListItem : DragAndDroppable
{
    public SpriteRenderer factoryImg;

    public void setFactory(Factory factory)
    {
        factoryImg.sprite = Resources.Load<Sprite>("Images/Factories/" + factory.type.name);
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "PersonnelListItem" };
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        Debug.Log("FactoryListItem onPointEnter");
    }
    protected override void onPointExit()
    {
        Debug.Log("FactoryListItem onPointExit");
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
