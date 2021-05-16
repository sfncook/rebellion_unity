using UnityEngine;

public class OpenStarChartBtn : MonoBehaviour
{
    public GameObject onSurfaceScrollView;
    public GameObject starChartPanel;
    public SpriteRenderer menuBtnImg;

    void OnMouseDown()
    {
        if (!starChartPanel.activeSelf)
        {
            menuBtnImg.sprite = Resources.Load<Sprite>("40+ Simple Icons - Free/Cross_Simple_Icons_UI");
            onSurfaceScrollView.SetActive(false);
            starChartPanel.SetActive(true);
        } else
        {
            menuBtnImg.sprite = Resources.Load<Sprite>("Clean Vector Icons/T_11_star_");
            onSurfaceScrollView.SetActive(true);
            starChartPanel.SetActive(false);
        }
        
    }
}
