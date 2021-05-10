using UnityEngine;
using TMPro;

public class TabLabel : MonoBehaviour
{
    private MainGameState gameState;

    // For updating color
    private TabType lastTabType;

    void Start()
    {
        gameState = MainGameState.gameState;
        updateLabel();
    }

    private void OnGUI()
    {
        if (gameState.selectedTab != lastTabType)
        {
            updateLabel();
            lastTabType = gameState.selectedTab;
        }
    }

    private void updateLabel()
    {
        switch(gameState.selectedTab)
        {
            case TabType.Personnel:
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Personnel";
                break;
            case TabType.Defense:
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Defense Structures";
                break;
            case TabType.Factory:
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Factories";
                break;
            case TabType.Ship:
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Ships and Fleets";
                break;
        }
    }
}
