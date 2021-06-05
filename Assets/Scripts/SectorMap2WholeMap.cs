using UnityEngine;
using UnityEngine.SceneManagement;

public class SectorMap2WholeMap : MonoBehaviour
{
    public HeaderControls headerControls;

    void Start()
    {
        headerControls.setHeaderTitle(MainGameState.gameState.sectorForDetail.name);
    }

    public void onClickPlanet(Planet planet)
    {
        MainGameState.gameState.planetForDetail = planet;
        SceneManager.LoadScene("Planet Detail");
    }
}
