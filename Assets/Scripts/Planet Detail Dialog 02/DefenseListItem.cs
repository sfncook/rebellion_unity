using UnityEngine;

public class DefenseListItem : MonoBehaviour
{
    public SpriteRenderer defenseImg;

    public void setDefense(Defense defense)
    {
        defenseImg.sprite = Resources.Load<Sprite>("Images/Defenses/" + defense.type.name);

        //if (personnel.team.Equals(Team.TeamA))
        //{
        //    personnelImg.transform.localScale = new Vector3(50, 50, 1);
        //    personnelImg.color = Color.green;
        //}
        //else
        //{
        //    personnelImg.transform.localScale = new Vector3(-50, 50, 1);
        //    personnelImg.color = Color.red;
        //}
    }
}
