using UnityEngine;

public class PersonnelListItem : DragAndDroppable
{
    public SpriteRenderer personnelImg;

    private MainGameState gameState;
    private Personnel personnel;

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
    }

    public Personnel getPersonnel()
    {
        return personnel;
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
