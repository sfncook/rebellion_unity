using UnityEngine;

public class TabButton : MonoBehaviour
{
    private MainGameState gameState;
    public PlanetDialog planetDialog;
    public TabType tabType;

    // For updating color
    private TabType lastTabType;

    void Start()
    {
        gameState = MainGameState.gameState;
        updateTabColor();
    }

    void OnMouseDown()
    {
        gameState.selectedTab = tabType;
    }

    private void OnGUI()
    {
        if(gameState.selectedTab != lastTabType)
        {
            updateTabColor();
            lastTabType = gameState.selectedTab;
        }
    }

    private void updateTabColor()
    {
        Color tabColor = Color.white;
        if (gameState.selectedTab == tabType)
        {
            tabColor = Color.blue;
        }
        gameObject.GetComponent<SpriteRenderer>().color = tabColor;
    }
}
