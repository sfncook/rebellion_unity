using System.Collections.Generic;
using UnityEngine;

public class FilterMenuStack : MonoBehaviour
{
    public Transform menuPanel;
    public GameObject menuItemPrefab;

    private List<FilterType> filterTypes = new List<FilterType>();

    void Start()
    {
        //hide();
        updateGrid();
    }

    private void updateGrid()
    {
        clearGrid();

        foreach (FilterType filterType in filterTypes)
        {
            GameObject newObj = (GameObject)Instantiate(menuItemPrefab, menuPanel);
            newObj.name = filterType.ToString();
            FilterMenuItem filterMenuItem= newObj.GetComponent<FilterMenuItem>();
            filterMenuItem.setFilterType(filterType);
            filterMenuItem.onClickFilterMenuItem = onClickFilterMenuItem;
        }
    }

    private void clearGrid()
    {
        foreach (Transform child in menuPanel)
        {
            UnityEngine.Object.Destroy(child.gameObject);
        }
    }

    public void setFilterTypes(List<FilterType> filterTypes)
    {
        this.filterTypes.Clear();
        this.filterTypes.AddRange(filterTypes);
        updateGrid();
    }

    public void show()
    {
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

    private void onClickFilterMenuItem(FilterType filterType)
    {

    }

}
