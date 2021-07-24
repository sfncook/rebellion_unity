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

    private void OnMouseUp()
    {
        onClickFilterMenuItem(filterType);
    }
}
