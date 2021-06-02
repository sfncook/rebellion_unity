using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class FactoryListItem : DragAndDroppable
{
    public Image factoryImg;
    public Image factoryIsWorkingIcon;

    private Factory factory;

    public delegate void OnClickFactoryHandler(Factory factory);
    private OnClickFactoryHandler onClickFactoryHandler;

    public void setFactory(Factory factory)
    {
        this.factory = factory;
        factoryImg.sprite = Resources.Load<Sprite>("Images/Factories/" + factory.type.name);
        factoryIsWorkingIcon.gameObject.SetActive(factory.isBuilding);
    }

    public void setOnClickFactoryHandler(OnClickFactoryHandler onClickFactoryHandler)
    {
        this.onClickFactoryHandler = onClickFactoryHandler;
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

    private void OnMouseUp()
    {
        onClickFactoryHandler(factory);
    }
}
