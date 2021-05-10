using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetDetailGrid : MonoBehaviour
{
    public GameObject prefab;
    public int manyToCreate;

    void Start()
    {
        populateGrid();
    }

    private void populateGrid()
    {
        GameObject newObj;

        for(int i=0; i<manyToCreate; i++)
        {
            newObj = (GameObject)Instantiate(prefab, transform);
            newObj.GetComponent<Image>().color = Random.ColorHSV();
        }
    }
}
