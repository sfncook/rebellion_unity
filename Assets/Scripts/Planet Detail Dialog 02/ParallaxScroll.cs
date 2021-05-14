using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public GameObject backgroundImg;
    public GameObject planetImg;

    private void Start()
    {
        updateParallax(0.0f);
    }

    public void onScroll(Vector2 vector2)
    {
        updateParallax(vector2.y);
    }

    private void updateParallax(float scrollY)
    {
        backgroundImg.transform.position = new Vector3(
            backgroundImg.transform.position.x,
            (-6.0f * scrollY * 0.08f),
            backgroundImg.transform.position.z
            );
        planetImg.transform.position = new Vector3(
            planetImg.transform.position.x,
            (-2.0f * scrollY) - 2.0f,
            planetImg.transform.position.z
            );
    }
}
