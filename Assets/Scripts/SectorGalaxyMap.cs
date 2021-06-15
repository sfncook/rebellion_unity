using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class OnClickSector: UnityEvent<StarSector>
{
}

public class SectorGalaxyMap : DragAndDroppable
{
    public TextMeshProUGUI sectorNameText;
    public GameObject planetPrefab;
    public bool isHoverable = false;
    public SectorMap2 sectorMap;
    public GameObject galaxyMap;
    public GameObject toGalaxyHoverPanel;

    private OnClickSector onClickSectorEvent;
    private StarSector sector;

    public void setSector(StarSector sector)
    {
        this.sector = sector;
        sectorNameText.text = sector.name;
        updateStars();
    }

    private void updateStars()
    {
        foreach(Planet planet in sector.planets)
        {
            GameObject newPlanetObj = (GameObject)Instantiate(planetPrefab, transform);
            RectTransform planetRectTrans = newPlanetObj.GetComponent<RectTransform>();
            planetRectTrans.anchorMin = new Vector2(planet.sectorX, planet.sectorY);
            planetRectTrans.anchorMax = new Vector2(planet.sectorX, planet.sectorY);
            planetRectTrans.anchoredPosition = new Vector2(0f, 0f);
        }
    }

    public void setOnClickSectorEvent(OnClickSector onClickSectorEvent)
    {
        this.onClickSectorEvent = onClickSectorEvent;
    }

    private void OnMouseUp()
    {
        onClickSectorEvent.Invoke(sector);
    }

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
        resetHoverState();
        sectorMap.setSector(sector);
        galaxyMap.SetActive(false);
        sectorMap.gameObject.SetActive(true);
        toGalaxyHoverPanel.SetActive(true);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.17f);
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

    protected override void onPointEnter(GameObject pointerDrag)
    {
        gameObject.GetComponent<Image>().color = new Color(0, 0, 1);
    }
    protected override void onPointExit()
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.17f);
    }

    public void setIsSelected()
    {
        gameObject.GetComponent<Image>().color = Color.yellow;
    }
}
