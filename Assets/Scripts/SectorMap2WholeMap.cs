using UnityEngine;
using UnityEngine.SceneManagement;

public class SectorMap2WholeMap : MonoBehaviour
{
    public HeaderControls headerControls;
    public SectorMap2 sectorMap;

    void Start()
    {
        headerControls.setHeaderTitle(MainGameState.gameState.sectorForDetail.name);
        sectorMap.setSector(MainGameState.gameState.sectorForDetail);
    }

    public void onClickPlanet(Planet planet)
    {
        if(planet.isDiscovered)
        {
            MainGameState.gameState.planetForDetail = planet;
            SceneManager.LoadScene("Planet Detail 2");
        }
    }

    public void onClickBackButton()
    {
        MainGameState.gameState.sectorForDetail = null;
        SceneManager.LoadScene("Galaxy Map");
    }
}
