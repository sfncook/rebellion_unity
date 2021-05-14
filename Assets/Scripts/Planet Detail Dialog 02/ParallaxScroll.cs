using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public GameObject backgroundImg;
    public GameObject planetImg;

    public void onScroll(Vector2 vector2)
    {
        backgroundImg.transform.position = new Vector3(
            backgroundImg.transform.position.x,
            (-6.0f * vector2.y * 0.08f),
            backgroundImg.transform.position.z
            );
        planetImg.transform.position = new Vector3(
            planetImg.transform.position.x,
            (-2.0f * vector2.y) - 2.0f,
            planetImg.transform.position.z
            );
    }
}
