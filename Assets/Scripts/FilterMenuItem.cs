using UnityEngine;
using TMPro;

public delegate void OnClickFilterMenuItem(FilterType filterType);

public class FilterMenuItem : MonoBehaviour
{
    public TextMeshProUGUI menuItemText;
    public OnClickFilterMenuItem onClickFilterMenuItem;

    private FilterType filterType;

    public void setFilterType(FilterType filterType)
    {
        this.filterType = filterType;
        menuItemText.text = FilterTypeHelper.filterLabels[filterType];
    }

    private void OnGUI()
    {
        if (Event.current.button == 0)
        {
            if (Event.current.type == EventType.MouseUp)
            {
                Debug.Log("FilterMenuItem.OnGUI MouseUp filterType:"+ filterType);
                //Event.current.Use();
                onClickFilterMenuItem(filterType);
            }
            else if (Event.current.type == EventType.MouseDown)
            {
                //Debug.Log("FilterMenuItem.OnGUI MouseDown");
                Event.current.Use();
            }
        }
    }
}
