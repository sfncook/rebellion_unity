using UnityEngine;


public class GalaxyMap : MonoBehaviour
{
    public GameObject sectorPrefab;
    public Transform mapPanel;

    public OnClickSector onClickSectorEvent;

    void Start()
    {
        Galaxy galaxy = MainGameState.gameState.galaxy;
        foreach(StarSector sector in galaxy.sectors)
        {
            GameObject newSectorObj = (GameObject)Instantiate(sectorPrefab, mapPanel);
            RectTransform sectorRectTrans = newSectorObj.GetComponent<RectTransform>();
            sectorRectTrans.anchorMin = new Vector2(sector.galaxyX, sector.galaxyY);
            sectorRectTrans.anchorMax = new Vector2(sector.galaxyX, sector.galaxyY);
            sectorRectTrans.anchoredPosition = new Vector2(0f, 0f);
            sectorRectTrans.localScale = new Vector2(2, 2);

            SectorGalaxyMap sectorGalaxyMap = newSectorObj.GetComponent<SectorGalaxyMap>();
            sectorGalaxyMap.setSector(sector);
            sectorGalaxyMap.setOnClickSectorEvent(onClickSectorEvent);
        }
    }
}
