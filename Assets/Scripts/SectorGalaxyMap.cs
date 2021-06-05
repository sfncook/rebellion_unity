using UnityEngine;

public class SectorGalaxyMap : MonoBehaviour
{
    public GameObject planetPrefab;

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
}
