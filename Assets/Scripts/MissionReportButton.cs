using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionReportButton : MonoBehaviour
{
    private void Update()
    {
        string imageFile;
        Color color;
        if (MainGameState.gameState.reportsUnAcked.Count > 0)
        {
            imageFile = "T_17_message_points_";
            color = Color.blue;
        } else
        {
            imageFile = "T_16_message_";
            color = Color.white;
        }
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Clean Vector Icons/" + imageFile);
        gameObject.GetComponent<Image>().color = color;
    }

    public void OnMouseUp()
    {
        SceneManager.LoadScene("Mission Report Dialog");
    }
}
