using UnityEngine;
using UnityEngine.UI;

public class FactoryListItem2 : MonoBehaviour
{
    public Image factoryImg;
    public Image backgroundImg;

    private Factory factory;

    public void setFactory(Factory factory, Team team)
    {
        this.factory = factory;

        factoryImg.sprite = Resources.Load<Sprite>("Images/Factories/" + factory.type.name);
        if (team.Equals(Team.TeamA))
        {
            backgroundImg.color = Color.green;
        }
        else if (team.Equals(Team.TeamB))
        {
            backgroundImg.color = Color.red;
        }
    }
}
