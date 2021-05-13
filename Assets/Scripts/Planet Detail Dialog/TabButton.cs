using UnityEngine;

public class TabButton : MonoBehaviour
{
    public TabType tabType;
    public PlanetDetailGrid planetDetailGrid;

    private TabType lastTabType;

    void Start()
    {
        updateTabColor();
    }

    void OnMouseDown()
    {
        planetDetailGrid.setSelectedTab(tabType);
    }

    private void OnGUI()
    {
        if(planetDetailGrid.selectedTab != lastTabType)
        {
            updateTabColor();
            lastTabType = planetDetailGrid.selectedTab;
        }
    }

    private void updateTabColor()
    {
        Color tabColor = Color.white;
        if (planetDetailGrid.selectedTab == tabType)
        {
            tabColor = Color.blue;
        }
        gameObject.GetComponent<SpriteRenderer>().color = tabColor;
    }
}
