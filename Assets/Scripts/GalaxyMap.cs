using UnityEngine;


public class GalaxyMap : MonoBehaviour
{
    public GameObject sectorPrefab;
    public float sectorScale = 2;
    public SectorMap2 sectorMap;
    public GameObject galaxyMap;
    public GameObject toGalaxyHoverPanel;

    public OnClickSector onClickSectorEvent;

    void Start()
    {
        MainGameState.gameState.initializeNewGame();
        Galaxy galaxy = MainGameState.gameState.galaxy;
        foreach(StarSector sector in galaxy.sectors)
        {
            GameObject newSectorObj = (GameObject)Instantiate(sectorPrefab, transform);
            newSectorObj.name = sector.name;
            RectTransform sectorRectTrans = newSectorObj.GetComponent<RectTransform>();
            sectorRectTrans.anchorMin = new Vector2(sector.galaxyX, sector.galaxyY);
            sectorRectTrans.anchorMax = new Vector2(sector.galaxyX, sector.galaxyY);
            sectorRectTrans.anchoredPosition = new Vector2(0f, 0f);
            sectorRectTrans.localScale = new Vector2(sectorScale, sectorScale);

            SectorGalaxyMap sectorGalaxyMap = newSectorObj.GetComponent<SectorGalaxyMap>();
            sectorGalaxyMap.setSector(sector);
            sectorGalaxyMap.setOnClickSectorEvent(onClickSectorEvent);
            sectorGalaxyMap.sectorMap = sectorMap;
            sectorGalaxyMap.galaxyMap = galaxyMap;
            sectorGalaxyMap.toGalaxyHoverPanel = toGalaxyHoverPanel;
        }
    }
}
