using UnityEngine;

public class SectorMap2 : MonoBehaviour
{
    public GameObject planetPrefab;
    public Transform panelForPlanets;
    public OnClickPlanet onClickPlanetEvent;

    void Start()
    {
        Planet ater = new Planet("Ater", Random.Range(1, 10), Random.Range(0.0f, 0.999f), 0.2f, 0.8f);
        Planet nageron = new Planet("Nageron", Random.Range(1, 10), Random.Range(0.0f, 0.999f), 0.8f, 0.77f);
        Planet ucholla = new Planet("Ucholla", Random.Range(1, 10), Random.Range(0.0f, 0.999f), 0.6f, 0.45f);
        Planet obiemia = new Planet("Obiemia", Random.Range(1, 10), Random.Range(0.0f, 0.999f), 0.3f, 0.25f);
        Planet ibos = new Planet("Ibos", Random.Range(1, 10), Random.Range(0.0f, 0.999f), 0.75f, 0.19f);

        instantiatePlanet(ater);
        instantiatePlanet(nageron);
        instantiatePlanet(ucholla);
        instantiatePlanet(obiemia);
        instantiatePlanet(ibos);
    }

    private void instantiatePlanet(Planet planet)
    {
        GameObject newPlanetObj = (GameObject)Instantiate(planetPrefab, transform);
        RectTransform planetRectTrans = newPlanetObj.GetComponent<RectTransform>();
        planetRectTrans.anchorMin = new Vector2(planet.sectorX, planet.sectorY);
        planetRectTrans.anchorMax = new Vector2(planet.sectorX, planet.sectorY);
        planetRectTrans.anchoredPosition = new Vector2(0f, 0f);

        PlanetMap2 planetMap2 = newPlanetObj.GetComponent<PlanetMap2>();
        planetMap2.setOnClickPlanetEvent(onClickPlanetEvent);
        planetMap2.setPlanet(planet);
    }
}
