using UnityEngine;

public class ShipUpdater : MonoBehaviour
{
    private MainGameState gameState;
    private Planet planet;

    void Start()
    {
        gameState = MainGameState.gameState;
        planet = gameState.getPlanetByName(gameObject.name);
        gameState.addListenerAgentPlanEvent(onAgentPlanEvent);
        gameState.addListenerAgentActionEvent(onAgentActionEvent);
        gameState.addListenerPostCleanupEvent(onPostCleanupEvent);
    }

    public void onAgentPlanEvent()
    {

    }

    public void onAgentActionEvent()
    {

    }

    public void onPostCleanupEvent()
    {

    }

}
