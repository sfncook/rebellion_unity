using UnityEngine;

public class LoyaltyBars2 : MonoBehaviour
{
    public Transform teamALoyaltyBar;
    public Transform teamBLoyaltyBar;

    public void setPlanet(Planet planet)
    {
        Debug.Log("LoyaltyBars2 Planet:" + planet.name + " Loyalty:" + planet.loyalty);
        float fullWidth = teamBLoyaltyBar.localScale.x;

        float scaleX = (1.0f - planet.loyalty) * fullWidth;
        float scaleY = teamALoyaltyBar.localScale.y;
        float scaleZ = teamALoyaltyBar.localScale.z;
        teamALoyaltyBar.localScale = new Vector3(scaleX, scaleY, scaleZ);

        float posX = scaleX / 2.0f;
        float posY = teamALoyaltyBar.localPosition.y;
        float posZ = teamALoyaltyBar.localPosition.z;
        teamALoyaltyBar.localPosition = new Vector3(posX, posY, posZ);
    }

}
