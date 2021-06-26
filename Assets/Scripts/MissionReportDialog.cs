using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionReportDialog : MonoBehaviour
{
    public Transform reportGridPanel;
    public GameObject missionRowPrefab;

    void Start()
    {
        MainGameState.gameState.stopTimerEvent.Invoke();
        updateGrid();
    }

    private void updateGrid()
    {
        clearGrid();

        foreach (Report report in MainGameState.gameState.reportsUnAcked)
        {
            GameObject newObj = (GameObject)Instantiate(missionRowPrefab, reportGridPanel);
            ReportRow reportRow = newObj.GetComponent<ReportRow>();
            reportRow.setReport(report);
            RectTransform rectTrans = reportRow.GetComponent<RectTransform>();
            rectTrans.sizeDelta = new Vector2(790f, 85f);
        }

        // Reset unacked list
        MainGameState.gameState.reportsUnAcked.Clear();
    }

    private void clearGrid()
    {
        foreach (Transform child in reportGridPanel)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void onClickBack()
    {
        if(MainGameState.gameState.planetForDetail!= null)
        {
            SceneManager.LoadScene("Planet Detail 2");
        } else if (MainGameState.gameState.sectorForDetail != null)
        {
            SceneManager.LoadScene("Sector Map 2");
        } else
        {
            SceneManager.LoadScene("Galaxy Map");
        }
    }
}
