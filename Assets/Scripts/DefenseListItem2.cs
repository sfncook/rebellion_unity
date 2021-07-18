using UnityEngine;
using UnityEngine.UI;

public class DefenseListItem2 : MonoBehaviour
{
    public Image defenseImg;
    public Image backgroundImg;
    public Image inTransitImg;
    public ValueBars healthBars;

    private Defense defense;

    public void setDefense(Defense defense, Team team)
    {
        this.defense = defense;

        defenseImg.sprite = Resources.Load<Sprite>("Images/Defenses/" + defense.type.name);
        if (team.Equals(Team.TeamA))
        {
            backgroundImg.color = Color.green;
        }
        else if (team.Equals(Team.TeamB))
        {
            backgroundImg.color = Color.red;
        }

        inTransitImg.gameObject.SetActive(defense.inTransit());

        updateHealthBars();
    }

    private void updateHealthBars()
    {
        float fullHealth = ((DefenseType)defense.type).fullHealth;
        float healthPercent = defense.health / fullHealth;
        Debug.Log("DefenseListItem defense.health:"+ defense.health+ " fullHealth:"+ fullHealth+ " healthPercent:"+ healthPercent);
        healthBars.setValue(healthPercent);
    }
}
