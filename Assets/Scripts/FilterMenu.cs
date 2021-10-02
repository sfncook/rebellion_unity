using UnityEngine;

public class FilterMenu : MonoBehaviour
{
    public void onClickFilterMenuItem(FilterType filterType)
    {
        MainGameState.gameState.setNewFilter(filterType);
    }
}
