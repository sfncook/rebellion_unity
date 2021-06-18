using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonnelListItem2 : DragAndDroppable
{
    public Image personnelImg;
    public Image backgroundImg;
    public Image inTransitImg;
    public TextMeshProUGUI manyPeopleLabel;

    private Personnel personnel;
    private Ship onShip = null;

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

        inTransitImg.gameObject.SetActive(personnel.inTransit());
    }

    public Personnel getPersonnel()
    {
        return personnel;
    }

    public void setOnShip(Ship onShip)
    {
        this.onShip = onShip;
    }
    public Ship getOnShip()
    {
        return onShip;
    }
    public bool isLocatedOnShip()
    {
        return onShip != null;
    }

    protected override bool isDraggable()
    {
        return MainGameState.gameState.myTeam == personnel.team &&
            (
                (isLocatedOnShip() && !onShip.inTransit()) ||
                !isLocatedOnShip()
            );
    }

    protected override bool isDroppable()
    {
        return false;
    }
}
