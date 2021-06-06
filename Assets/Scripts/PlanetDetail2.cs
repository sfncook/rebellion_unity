using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlanetDetail2 : MonoBehaviour
{
    public HeaderControls headerControls;
    public Image starBackgroundImg;
    public Image planetImg;
    public GameObject teamAInOrbitPanel;
    public GameObject teamBInOrbitPanel;
    public GameObject teamAOnSurfacePanel;
    public GameObject teamBOnSurfacePanel;
    public GameObject infrastructurePanel;

    private Planet planet;

    void Start()
    {
        planet = MainGameState.gameState.planetForDetail;
        headerControls.setHeaderTitle(planet.name);
    }

    private void updateUnits()
    {
        
    }

    public void onClickBackButton()
    {
        MainGameState.gameState.planetForDetail = null;
        SceneManager.LoadScene("Sector Map 2");
    }
}
