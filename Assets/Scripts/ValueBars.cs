using UnityEngine;

public class ValueBars : MonoBehaviour
{
    public Transform valueBar;
    public Transform backgroundBar;

    public void setValue(float value)
    {
        RectTransform bgRect = backgroundBar.GetComponent<RectTransform>();
        float fullWidth = bgRect.sizeDelta.x;

        float valueWidth = value * fullWidth;
        RectTransform r = valueBar.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(valueWidth, r.sizeDelta.y);

        float posX = (valueWidth / 2.0f) - (fullWidth / 2.0f);
        r.localPosition = new Vector3(posX, r.localPosition.y, r.localPosition.z);
    }

}
