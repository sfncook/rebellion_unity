using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("Sector Map");
    }
}
