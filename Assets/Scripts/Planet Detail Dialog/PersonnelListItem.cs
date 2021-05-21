using UnityEngine;

public class PersonnelListItem : DragAndDroppable
{
    public SpriteRenderer personnelImg;

    private MainGameState gameState;
    private Personnel personnel;
    private StartDraggingPersonnel startDraggingPersonnel;
    private StopDraggingPersonnel stopDraggingPersonnel;

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

    public void setStartDraggingPersonnel(StartDraggingPersonnel startDraggingPersonnel)
    {
        this.startDraggingPersonnel = startDraggingPersonnel;
    }
    public void setStopDraggingPersonnel(StopDraggingPersonnel stopDraggingPersonnel)
    {
        this.stopDraggingPersonnel = stopDraggingPersonnel;
    }

    protected override bool isDraggable()
    {
        return gameState.myTeam == personnel.team;
    }

    protected override bool isDroppable()
    {
        return false;
    }

    protected override void onDragStart()
    {
        if(startDraggingPersonnel!=null) startDraggingPersonnel.Invoke();
    }

    protected override void onDragStop()
    {
        if (stopDraggingPersonnel != null) stopDraggingPersonnel.Invoke();
    }
}
