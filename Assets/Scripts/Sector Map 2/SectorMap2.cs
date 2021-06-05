using UnityEngine;

public class SectorMap2 : MonoBehaviour
{
    public GameObject planetPrefab;
    public Transform panelForPlanets;
    public OnClickPlanet onClickPlanetEvent;

    void Start()
    {
        foreach(Planet planet in MainGameState.gameState.sectorForDetail.planets)
        {
            instantiatePlanet(planet);
        }
    }

    private void instantiatePlanet(Planet planet)
    {
        GameObject newPlanetObj = (GameObject)Instantiate(planetPrefab, transform);
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