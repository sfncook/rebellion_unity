using UnityEngine;
using TMPro;

public class FilterMenu : MonoBehaviour
{
    public TextMeshProUGUI headerTitleText;

    private void Start()
    {
        updateFilter(MainGameState.gameState.selectedFilterType);
    }

    public void onClickFilterMenuItem(FilterType filterType)
    {
        updateFilter(filterType);
    }

    private void updateFilter(FilterType filterType)
    {
        MainGameState.gameState.setNewFilter(filterType);
        headerTitleText.text = FilterTypeHelper.filterLabels[filterType];
    }
}
