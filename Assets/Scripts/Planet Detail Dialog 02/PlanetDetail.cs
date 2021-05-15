using UnityEngine;
using TMPro;

public class PlanetDetail : MonoBehaviour
{
    public TextMeshProUGUI planetNameLabel;
    public SpriteRenderer imgFactory;


    private MainGameState gameState;
    private Planet planet;

    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.planetForDetail;
        imgFactory.sprite = Resources.Load<Sprite>("Images/Planets/"+planet.name);
        planetNameLabel.text = planet.name;
    }
}
