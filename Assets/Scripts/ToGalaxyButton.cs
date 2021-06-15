using UnityEngine;
using TMPro;

public class ToGalaxyButton : MonoBehaviour
{
    public GameObject galaxyMap;
    public GameObject sectorMap;
    public TextMeshProUGUI sectorNameText;

    public void OnMouseUp()
    {
        galaxyMap.SetActive(true);
        sectorMap.SetActive(false);
        gameObject.SetActive(false);
        sectorNameText.gameObject.SetActive(false);
    }
}
