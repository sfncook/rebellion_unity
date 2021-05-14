using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public GameObject background;

    public void onScroll(Vector2 vector2)
    {
        background.transform.position = new Vector3(
            background.transform.position.x,
            (-6.0f * vector2.y * 0.1f),
            background.transform.position.z
            );
    }
}
