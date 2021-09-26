using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnClickClickAwayPanel : UnityEvent
{
}

public class FilterMenuClickAwayPanel : MonoBehaviour
{
    public OnClickClickAwayPanel onClickClickAwayPanel;

    private void OnGUI()
    {
        if (Event.current.button == 0)
        {
            if (Event.current.type == EventType.MouseUp)
            {
                onClickClickAwayPanel.Invoke();
                Event.current.Use();
            } else if (Event.current.type == EventType.MouseDown)
            {
                Event.current.Use();
            }
        }
    }
}
