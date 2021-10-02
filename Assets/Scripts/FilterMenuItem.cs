using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class OnClickFilterMenuItem : UnityEvent<FilterType> { }

public class FilterMenuItem : MonoBehaviour
{
    public OnClickFilterMenuItem onClickFilterMenuItem;
    public FilterType filterType;

    private void Start()
    {
        MainGameState.gameState.filterChangeEvent.AddListener(onFilterChange);
        updateColor();
    }

    private void onFilterChange(FilterType _newFilterType)
    {
        updateColor();
    }

    private void OnMouseUp()
    {
        onClickFilterMenuItem.Invoke(filterType);
    }

    private void updateColor()
    {
        Transform img = transform.Find("IconImg");
        if (MainGameState.gameState.selectedFilterType == filterType)
        {
            img.gameObject.GetComponent<RawImage>().color = new Color(0.49f, 0.92f, 1f);
        }
        else
        {
            img.gameObject.GetComponent<RawImage>().color = Color.white;
        }
    }
}
