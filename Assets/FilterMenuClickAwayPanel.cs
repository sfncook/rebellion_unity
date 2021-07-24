using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnClickClickAwayPanel : UnityEvent
{
}

public class FilterMenuClickAwayPanel : MonoBehaviour
{
    public OnClickClickAwayPanel onClickClickAwayPanel;

    private void OnMouseUp()
    {
        onClickClickAwayPanel.Invoke();
    }
}
