using UnityEngine;

public class TabButton : MonoBehaviour
{
    public PlanetDialog planetDialog;
    public TabType tabType;

    // For updating color
    private TabType lastTabType;

    void Start()
    {
        //energySquare.GetComponent<SpriteRenderer>().color = Color.blue;

    }

    void OnMouseDown()
    {
        planetDialog.selectedTab = tabType;
    }

    private void OnGUI()
    {
        if(planetDialog.selectedTab != lastTabType)
        {
            Color tabColor = Color.white;
            if(planetDialog.selectedTab == tabType)
            {
                tabColor = Color.blue;
            }
            gameObject.GetComponent<SpriteRenderer>().color = tabColor;
            lastTabType = planetDialog.selectedTab;
        }
    }
}
