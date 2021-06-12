using UnityEngine;
using UnityEngine.UI;

public class SectorMap2 : MonoBehaviour
{
    public GameObject planetPrefab;
    public Transform panelForPlanets;
    public Image starsBackgroundImg;

    public OnClickPlanet onClickPlanetEvent;

    private StarSector sector;

    public void setSector(StarSector sector)
    {
        this.sector = sector;
        starsBackgroundImg.sprite = Resources.Load<Sprite>("Images/Stars/" + sector.name);

        foreach (Planet planet in sector.planets)
        {
            instantiatePlanet(planet);
        }
    }

    private void instantiatePlanet(Planet planet)
    {
        GameObject newPlanetObj = (GameObject)Instantiate(planetPrefab, transform);
        newPlanetObj.name = planet.name;
        RectTransform planetRectTrans = newPlanetObj.GetComponent<RectTransform>();
        planetRectTrans.anchorMin = new Vector2(planet.sectorX, planet.sectorY);
        planetRectTrans.anchorMax = new Vector2(planet.sectorX, planet.sectorY);
        planetRectTrans.anchoredPosition = new Vector2(0f, 0f);
        planetRectTrans.localScale = new Vector2(2, 2);

        PlanetMap2 planetMap2 = newPlanetObj.GetComponent<PlanetMap2>();
        planetMap2.setOnClickPlanetEvent(onClickPlanetEvent);
        planetMap2.setPlanet(planet);
    }
}
