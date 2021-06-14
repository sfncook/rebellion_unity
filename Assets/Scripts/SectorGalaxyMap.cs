using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class OnClickSector: UnityEvent<StarSector>
{
}

public class SectorGalaxyMap : DragAndDroppable
{
    public GameObject planetPrefab;
    public bool isHoverable = false;

    private OnClickSector onClickSectorEvent;
    private StarSector sector;

    public void setSector(StarSector sector)
    {
        this.sector = sector;
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
}
