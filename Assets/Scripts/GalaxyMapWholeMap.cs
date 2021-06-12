using UnityEngine;
using UnityEngine.SceneManagement;

public class GalaxyMapWholeMap : MonoBehaviour
{
    public void onClickSector(StarSector sector)
    {
        MainGameState.gameState.sectorForDetail = sector;
        SceneManager.LoadScene("Sector Map 2");
    }
}
