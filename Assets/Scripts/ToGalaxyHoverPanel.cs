using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToGalaxyHoverPanel : DragAndDroppable
{
    public GameObject sectorMap;
    public GameObject galaxyMap;

    protected override List<string> acceptedDropTypes()
    {
        return new List<string>() { "ShipListItem2" };
    }

    protected override bool isDroppable()
    {
        return true;
    }

    protected override void onHoverTimerExpire()
    {
        gameObject.SetActive(false);
        galaxyMap.SetActive(true);
        sectorMap.SetActive(false);
        gameObject.GetComponent<Image>().color = Color.blue;
    }

    protected override void onPointExit()
    {
        gameObject.GetComponent<Image>().color = Color.blue;
    }

    void FixedUpdate()
    {
        _FixedUpdate();
        if (isHovering)
        {
            float g = 1.0f - (hoverTimerSec / HOVER_TIMER_DUR_SEC);
            gameObject.GetComponent<Image>().color = new Color(0, g, 1);
        }
    }

    protected override void onDrop(GameObject pointerDrag)
    {
        gameObject.GetComponent<Image>().color = Color.blue;
    }
}
