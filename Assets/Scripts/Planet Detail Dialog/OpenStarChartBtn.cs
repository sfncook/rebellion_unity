using UnityEngine;

public class OpenStarChartBtn : MonoBehaviour
{
    public GameObject inOrbitScrollView;
    public GameObject onSurfaceScrollView;
    public GameObject starChartPanel;
    public SpriteRenderer menuBtnImg;

    void OnMouseDown()
    {
        ShipListItem[] shipListItems = inOrbitScrollView.GetComponentsInChildren<ShipListItem>();
        if (!starChartPanel.activeSelf)
        {
            // Activate star chart
            menuBtnImg.sprite = Resources.Load<Sprite>("40+ Simple Icons - Free/Cross_Simple_Icons_UI");
            onSurfaceScrollView.SetActive(false);
            starChartPanel.SetActive(true);
            // Ships are draggable and not droppable when star chart is active
            foreach(ShipListItem shipListItem in shipListItems)
            {
                shipListItem.setIsDraggable(true);
                shipListItem.setIsDroppable(false);
            }
        } else
        {
            // Activate surface scroll view
            menuBtnImg.sprite = Resources.Load<Sprite>("Clean Vector Icons/T_11_star_");
            onSurfaceScrollView.SetActive(true);
            starChartPanel.SetActive(false);
            // Ships are droppable and not draggable when surface scroll view is active
            foreach (ShipListItem shipListItem in shipListItems)
            {
                shipListItem.setIsDraggable(false);
                shipListItem.setIsDroppable(true);
            }
        }
        
    }
}
