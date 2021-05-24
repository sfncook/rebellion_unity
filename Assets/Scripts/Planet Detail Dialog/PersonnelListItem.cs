using UnityEngine;
using TMPro;

public class PersonnelListItem : DragAndDroppable
{
    public SpriteRenderer personnelImg;
    public TextMeshProUGUI manyPeopleLabel;

    private MainGameState gameState;
    private Personnel personnel;
    private bool locatedOnShip = false;

    private void Start()
    {
        gameState = MainGameState.gameState;
    }

    public void setPersonnel(Personnel personnel)
    {
        this.personnel = personnel;
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

        int totalManyPeople = ((PersonnelType)personnel.type).totalManyPeople;
        if (totalManyPeople > 1)
        {
            manyPeopleLabel.gameObject.SetActive(true);
            manyPeopleLabel.text = personnel.manyPeople.ToString();
        } else
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
        return gameState.myTeam == personnel.team;
    }

    protected override bool isDroppable()
    {
        return false;
    }
}
