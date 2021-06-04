using UnityEngine;

public class SectorMap2 : MonoBehaviour
{
    public GameObject planetPrefab;
    public Transform panelForPlanets;

    void Start()
    {
        GameObject newPlanetObj = (GameObject)Instantiate(planetPrefab, panelForPlanets);
        RectTransform planetRectTrans = newPlanetObj.GetComponent<RectTransform>();
        planetRectTrans.anchorMin = new Vector2(0.75f, 0.75f);
        planetRectTrans.anchorMax = new Vector2(0.75f, 0.75f);
        planetRectTrans.anchoredPosition = new Vector2(0f, 0f);
    }
}
