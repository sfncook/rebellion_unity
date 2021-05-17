using UnityEngine;

public class CloseShipContentsBtn : MonoBehaviour
{
    public ShipContentsPanel shipContentsPanel;

    void OnMouseDown()
    {
        shipContentsPanel.hideShipContents();
    }
}
