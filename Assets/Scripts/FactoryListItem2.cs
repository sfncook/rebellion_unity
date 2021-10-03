using UnityEngine;
using UnityEngine.UI;

public class FactoryListItem2 : MonoBehaviour
{
    public Image factoryImg;
    public Image backgroundImg;
    public Image inTransitImg;
    public Image factoryIsWorkingIcon;

    public delegate void OnClickFactoryHandler(Factory factory);
    private OnClickFactoryHandler onClickFactoryHandler;

    private Factory factory;

    public void setFactory(Factory factory, Team team)
    {
        this.factory = factory;
        factoryIsWorkingIcon.gameObject.SetActive(factory.isBuilding);

        factoryImg.sprite = Resources.Load<Sprite>("Images/Factories/" + factory.type.name);
        if (team.Equals(Team.TeamA))
        {
            backgroundImg.color = Color.green;
        }
        else if (team.Equals(Team.TeamB))
        {
            backgroundImg.color = Color.red;
        }

        inTransitImg.gameObject.SetActive(factory.inTransit());
    }

    public void setOnClickFactoryHandler(OnClickFactoryHandler onClickFactoryHandler)
    {
        this.onClickFactoryHandler = onClickFactoryHandler;
    }

    private void OnMouseUp()
    {
        onClickFactoryHandler(factory);
    }

    private void FixedUpdate()
    {
        if(factory.isBuilding && MainGameState.gameState.getIsTimerRunning())
        {
            factoryIsWorkingIcon.transform.Rotate(Vector3.forward * (Time.deltaTime * 180.0f));
        }
    }
}
