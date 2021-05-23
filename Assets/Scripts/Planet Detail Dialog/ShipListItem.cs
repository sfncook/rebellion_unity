using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShipListItem : DragAndDroppable
{
    public SpriteRenderer shipImg;
    public SpriteRenderer hasPersonnelImg;
    public Image bgColor;

    private MainGameState gameState;
    private Ship ship;
    private ShowShipContentsEvent showShipContentsEvent;
    private bool isDragging = false;

    public delegate void RemovePersonnel(Personnel personnel);
    private RemovePersonnel removePersonnelCallback;

    public delegate void StartMoveShip();
    private StartMoveShip startMoveShip;

    public delegate void StopMoveShip();
    private StopMoveShip stopMoveShip;


    private void Start()
    {
        gameState = MainGameState.gameState;
    }

    public void setRemovePersonnelDelegate(RemovePersonnel removePersonnel)
    {
        this.removePersonnelCallback = removePersonnel;
    }

    public void setShowShipContentsEvent(ShowShipContentsEvent showShipContentsEvent)
    {
        this.showShipContentsEvent = showShipContentsEvent;
    }

    public void setStartMoveShip(StartMoveShip startMoveShip)
    {
        this.startMoveShip = startMoveShip;
    }

    public void setStopMoveShip(StopMoveShip stopMoveShip)
    {
        this.stopMoveShip = stopMoveShip;
    }

    public void setShip(Ship ship)
    {
        this.ship = ship;
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name);
        hasPersonnelImg.enabled = ship.personnelsOnBoard.Count > 0;

        if (ship.team.Equals(Team.TeamA))
        {
            shipImg.color = Color.green;
        }
        else
        {
            shipImg.transform.localScale = new Vector3(
                -shipImg.transform.localScale.x,
                shipImg.transform.localScale.y,
                shipImg.transform.localScale.z
                );
            shipImg.color = Color.red;
        }
    }
    public Ship getShip()
    {
        return ship;
    }

    protected override List<string> acceptedDropTypes()
    {
        if (gameState.myTeam == ship.team)
        {
            return new List<string>() { "PersonnelListItem" };
        }
        else
        {
            return new List<string>() { };
        }
    }

    protected override void onDrop(GameObject pointerDrag)
    {
        if (gameState.myTeam == ship.team)
        {
            PersonnelListItem personnelListItem = pointerDrag.GetComponent<PersonnelListItem>();
            if (
                personnelListItem.getPersonnel().team == ship.team &&
                ship.personnelsOnBoard.Count < ((ShipType)ship.type).personnelCapacity
                )
            {
                ship.personnelsOnBoard.Add(personnelListItem.getPersonnel());
                removePersonnelCallback(personnelListItem.getPersonnel());
            }
        }
    }

    protected override void onPointEnter(GameObject pointerDrag)
    {
        if (gameState.myTeam == ship.team)
        {
            PersonnelListItem personnelListItem = pointerDrag.GetComponent<PersonnelListItem>();
            if (
                    personnelListItem.getPersonnel().team == ship.team &&
                    ship.personnelsOnBoard.Count < ((ShipType)ship.type).personnelCapacity
                    )
            {
                bgColor.color = new Color(1.0f, 1.0f, 0.5f, 0.6f);
            }
        }
    }

    protected override void onPointExit()
    {
        bgColor.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    protected override bool isDraggable()
    {
        return gameState.myTeam == ship.team;
    }

    protected override bool isDroppable()
    {
        return gameState.myTeam == ship.team;
    }

    protected override void onDragStart()
    {
        isDragging = true;
        startMoveShip.Invoke();
    }

    protected override void onDragStop()
    {
        isDragging = false;
        stopMoveShip.Invoke();
    }

    private void OnMouseUp()
    {
        if (!isDragging)
        {
            if (gameState.myTeam == ship.team)
            {
                showShipContentsEvent.Invoke(ship);
            }
        }
    }
}
