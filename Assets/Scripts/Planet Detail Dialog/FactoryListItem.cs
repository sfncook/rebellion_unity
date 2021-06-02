using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class FactoryListItem : DragAndDroppable
{
    public Image factoryImg;
    public Image factoryIsWorkingIcon;

    private FactoryDialog factoryDialog;
    private Factory factory;

    public void setFactory(Factory factory)
    {
        this.factory = factory;
        factoryImg.sprite = Resources.Load<Sprite>("Images/Factories/" + factory.type.name);
        factoryIsWorkingIcon.gameObject.SetActive(factory.isBuilding);
    }

    public void setFactoryDialog(FactoryDialog factoryDialog)
    {
        this.factoryDialog = factoryDialog;
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
        Planet selectedPlanet = MainGameState.gameState.planetForDetail;
        if(selectedPlanet.loyalty<0.5f)
        {
            factoryDialog.setFactory(factory);
            factoryDialog.show();
        }
    }
}
