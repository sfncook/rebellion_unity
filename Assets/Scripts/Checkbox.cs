using UnityEngine;
using TMPro;

public class Checkbox : MonoBehaviour
{
    public TextMeshProUGUI checkText;
    public bool isChecked = false;
    
    void Start()
    {
        updateCheck();
    }

    private void updateCheck()
    {
        checkText.gameObject.SetActive(isChecked);
    }

    public void OnMouseUp()
    {
        isChecked = !isChecked;
        updateCheck();
    }
}
