using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterMenuStack : MonoBehaviour
{
    public Transform menuPanel;
    public GameObject menuItemPrefab;
    public OnClickFilterMenuItem onClickFilterMenuItem;
    public List<FilterType> filterTypes = new List<FilterType>();

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
            FilterMenuItem filterMenuItem= newObj.GetComponent<FilterMenuItem>();
            //RectTransform rectTrans = filterMenuItem.GetComponent<RectTransform>();
            //rectTrans.sizeDelta = new Vector2(760f, 100f);
            filterMenuItem.setFilterType(filterType);
            filterMenuItem.onClickFilterMenuItem = onClickFilterMenuItem;
        }
    }

    private void clearGrid()
    {
        foreach (Transform child in menuPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void show()
    {
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

}
