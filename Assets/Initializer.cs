using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject planetPrefab;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        GameObject p1 = Instantiate(planetPrefab, percToPos(0.25f, 0.25f), Quaternion.identity);

        var scale = new Vector2(0.06f, 0.06f);
        p1.transform.localScale = scale;
    }

    Vector2 percToPos(float xp, float yp)
    {
        float xw = (xp * Screen.width) - (Screen.width / 2.0f);
        float yw = (yp * Screen.height) - (Screen.height / 2.0f);
        var loc = cam.ScreenToWorldPoint(new Vector2(xw, yw));
        loc.z = 50f;
        return loc;
    }
}
