using UnityEngine;

public class SectorMap2WholeMap : MonoBehaviour
{
    public HeaderControls headerControls;

    void Start()
    {
        headerControls.setHeaderTitle(MainGameState.gameState.sectorForDetail.name);
    }
}
