using UnityEngine;

public class PersonnelListItem : MonoBehaviour
{
    public SpriteRenderer personnelImg;

    public void setPersonnel(Personnel personnel)
    {
        personnelImg.sprite = Resources.Load<Sprite>("Images/Personnel/" + personnel.type.name);

        if (personnel.team.Equals(Team.TeamA))
        {
            personnelImg.transform.localScale = new Vector3(50, 50, 1);
            personnelImg.color = Color.green;
        }
        else
        {
            personnelImg.transform.localScale = new Vector3(-50, 50, 1);
            personnelImg.color = Color.red;
        }
    }
}
