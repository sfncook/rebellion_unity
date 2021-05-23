using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class FactoryListItem : DragAndDroppable
{
    public SpriteRenderer factoryImg;

    public void setFactory(Factory factory)
    {
        factoryImg.sprite = Resources.Load<Sprite>("Images/Factories/" + factory.type.name);
    }

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { };
    }

    //protected override void onPointEnter(GameObject pointerDrag)
    //{
    //    factoryImg.gameObject.GetComponent<Image>().color = Color.yellow;
    //}
    //protected override void onPointExit()
    //{
    //    factoryImg.gameObject.GetComponent<Image>().color = Color.white;
    //}

    //protected override void onDrop(GameObject pointerDrag)
    //{
    //    factoryImg.gameObject.GetComponent<Image>().color = Color.white;
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
