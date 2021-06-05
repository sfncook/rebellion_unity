using UnityEngine;
using UnityEngine.SceneManagement;

public class GalaxyMap : MonoBehaviour
{
    public GameObject sectorPrefab;
    public Transform mapPanel;

    void Start()
    {
        Galaxy galaxy = MainGameState.gameState.galaxy;
        foreach(StarSector sector in galaxy.sectors)
        {
            Debug.Log(sector.name+" "+ sector.galaxyX+", "+ sector.galaxyY);
            GameObject newSectorObj = (GameObject)Instantiate(sectorPrefab, mapPanel);
            RectTransform sectorRectTrans = newSectorObj.GetComponent<RectTransform>();
            sectorRectTrans.anchorMin = new Vector2(sector.galaxyX, sector.galaxyY);
            sectorRectTrans.anchorMax = new Vector2(sector.galaxyX, sector.galaxyY);
            sectorRectTrans.anchoredPosition = new Vector2(0f, 0f);
            sectorRectTrans.localScale = new Vector2(2, 2);

            SectorGalaxyMap sectorGalaxyMap = newSectorObj.GetComponent<SectorGalaxyMap>();
            sectorGalaxyMap.setSector(sector);
            sectorGalaxyMap.setOnClickSector(onClickSector);
        }
    }

    public void onClickSector(StarSector sector)
    {
        MainGameState.gameState.sectorForDetail = sector;
        SceneManager.LoadScene("Sector Map 2");
    }
}
