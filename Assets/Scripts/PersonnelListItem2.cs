using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonnelListItem2 : DragAndDroppable
{
    public Image personnelImg;
    public Image backgroundImg;
    public TextMeshProUGUI manyPeopleLabel;

    private Personnel personnel;
    private bool locatedOnShip = false;

    public void setPersonnel(Personnel personnel)
    {
        this.personnel = personnel;
        personnelImg.sprite = Resources.Load<Sprite>("Images/Personnel/" + personnel.type.name);

        if (personnel.team.Equals(Team.TeamA))
        {
            backgroundImg.color = Color.green;
        }
        else
        {
            personnelImg.transform.localScale = new Vector3(-1, 1, 1);
            backgroundImg.color = Color.red;
        }

        int totalManyPeople = ((PersonnelType)personnel.type).totalManyPeople;
        if (totalManyPeople > 1)
        {
            manyPeopleLabel.gameObject.SetActive(true);
            manyPeopleLabel.text = personnel.manyPeople.ToString();
        }
        else
        {
            manyPeopleLabel.gameObject.SetActive(false);
        }
    }

    public Personnel getPersonnel()
    {
        return personnel;
    }

    public void setLocatedOnShip(bool locatedOnShip)
    {
        this.locatedOnShip = locatedOnShip;
    }
    public bool getLocatedOnShip()
    {
        return locatedOnShip;
    }

    protected override bool isDraggable()
    {
        return MainGameState.gameState.myTeam == personnel.team;
    }

    protected override bool isDroppable()
    {
        return false;
    }
}