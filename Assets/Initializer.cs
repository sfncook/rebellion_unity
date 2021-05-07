using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject planetPrefab;

    void Start()
    {
        Instantiate(planetPrefab, new Vector2(0, 0), Quaternion.identity);
    }
}
