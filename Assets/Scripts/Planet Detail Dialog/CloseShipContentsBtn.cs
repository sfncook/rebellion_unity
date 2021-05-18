using UnityEngine;

public class CloseShipContentsBtn : MonoBehaviour
{
    public ShipContentsAndMovePanel ShipContentsAndMovePanel;

    void OnMouseDown()
    {
        ShipContentsAndMovePanel.hideShipContents();
    }
}
