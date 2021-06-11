using UnityEngine;

public class LoyaltyBars2 : MonoBehaviour
{
    public Transform valueBar;
    public Transform backgroundBar;

    private float i = 0.0f;

    public void setPlanet(Planet planet)
    {
        //Debug.Log("LoyaltyBars2 Planet:" + planet.name + " Loyalty:" + planet.loyalty);
        //float fullWidth = teamBLoyaltyBar.localScale.x;

        //float scaleX = (1.0f - planet.loyalty) * fullWidth;
        //float scaleY = teamALoyaltyBar.localScale.y;
        //float scaleZ = teamALoyaltyBar.localScale.z;
        //teamALoyaltyBar.localScale = new Vector3(scaleX, scaleY, scaleZ);

        //float posX = scaleX / 2.0f;
        //float posY = teamALoyaltyBar.localPosition.y;
        //float posZ = teamALoyaltyBar.localPosition.z;
        //teamALoyaltyBar.localPosition = new Vector3(posX, posY, posZ);
    }

    void FixedUpdate()
    {
        RectTransform bgRect = backgroundBar.GetComponent<RectTransform>();
        float fullWidth = bgRect.sizeDelta.x;

        float valueWidth = i * fullWidth;
        RectTransform r = valueBar.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(valueWidth, r.sizeDelta.y);

        float posX = (valueWidth / 2.0f) - (fullWidth / 2.0f);
        r.localPosition = new Vector3(posX, r.localPosition.y, r.localPosition.z);

        i += 0.01f;
        if (i > 1.0f)
        {
            i = 0.0f;
        }
    }

}
