using UnityEngine;

public class ShipMoveStarChart : MonoBehaviour
{
    public SectorMap2 sectorMap;

    public void showSector(StarSector sector)
    {
        sectorMap.setSector(sector);
    }
}
