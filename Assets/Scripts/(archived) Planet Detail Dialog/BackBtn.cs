using UnityEngine;
using UnityEngine.SceneManagement;

public class BackBtn : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("Sector");
    }
}
