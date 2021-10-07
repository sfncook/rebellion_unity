using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.Text;

public class EventLogPanel : MonoBehaviour
{
    public TextMeshProUGUI eventMessages;
    public TextMeshProUGUI days;

    void Start()
    {
        MainGameState.gameState.addListenerUiUpdateEvent(onUiUpdate);
        onUiUpdate();
    }

    private void onUiUpdate()
    {
        int gameTime = MainGameState.gameState.gameTime;
        List<string> eventMsgs = MainGameState.gameState.getEventsForTime(gameTime);
        StringBuilder msgSb = new StringBuilder();
        StringBuilder timeSb = new StringBuilder();
        foreach(string msg in eventMsgs)
        {
            msgSb.Append(msg);
            msgSb.Append("\n");
            timeSb.Append(gameTime);
            timeSb.Append("\n");
        }
        eventMessages.text = msgSb.ToString();
        days.text = timeSb.ToString();
    }
}
