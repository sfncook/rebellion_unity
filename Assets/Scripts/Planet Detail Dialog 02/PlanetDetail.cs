using UnityEngine;
using TMPro;

public class PlanetDetail : MonoBehaviour
{
    public TextMeshProUGUI planetNameLabel;
    public SpriteRenderer planetImg;
    public SpriteRenderer planetShieldImg;


    private MainGameState gameState;
    private Planet planet;

    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.planetForDetail;
        planetImg.sprite = Resources.Load<Sprite>("Images/Planets/"+planet.name);
        planetNameLabel.text = planet.name;
        planetShieldImg.enabled = false;
    }
}
