using UnityEngine;
using TMPro;

public class TabLabel : MonoBehaviour
{
    public PlanetDetailGrid planetDetailGrid;

    // For updating color
    private TabType lastTabType;

    void Start()
    {
        updateLabel();
    }

    private void OnGUI()
    {
        if (planetDetailGrid.selectedTab != lastTabType)
        {
            updateLabel();
            lastTabType = planetDetailGrid.selectedTab;
        }
    }

    private void updateLabel()
    {
        switch(planetDetailGrid.selectedTab)
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
