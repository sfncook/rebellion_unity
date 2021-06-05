using UnityEngine;

public class GalaxyMap : MonoBehaviour
{
    void Start()
    {
        Debug.Log("many sectors:"+MainGameState.gameState.galaxy.sectors.Count);
    }
}
