using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShipListItem2 : DragAndDroppable
{
    public Image shipImg;
    public ValueBars healthBars;
    public Image bgColor;
    public Image hasPersonnelImg;

    private Ship ship;
    private bool isDragging = false;

    private ShowShipContentsEvent2 showShipContentsEvent;

    public delegate void RemovePersonnel(Personnel personnel);
    private RemovePersonnel removePersonnelCallback;

    public delegate void StartMoveShip();
    private StartMoveShip startMoveShip;

    public delegate void StopMoveShip();
    private StopMoveShip stopMoveShip;

    public void setShip(Ship ship)
    {
        this.ship = ship;
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name);
        hasPersonnelImg.gameObject.SetActive(ship.personnelsOnBoard.Count > 0);

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

        updateHealthBars();
    }
    public Ship getShip()
    {
        return ship;
    }

    private void updateHealthBars()
    {
        float fullHelath = ((ShipType)ship.type).fullHealth;
        float healthPercent = ship.health / fullHelath;
        healthBars.setValue(healthPercent);
    }


    public void setRemovePersonnelDelegate(RemovePersonnel removePersonnel)
    {
        this.removePersonnelCallback = removePersonnel;
    }

    public void setShowShipContentsEvent(ShowShipContentsEvent2 showShipContentsEvent)
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

    protected override List<string> acceptedDropTypes()
    {
        if (MainGameState.gameState.myTeam == ship.team)
        {
            return new List<string>() { "PersonnelListItem2" };
        }
        else
        {
            return new List<string>() { };
        }
    }

    protected override void onDrop(GameObject pointerDrag)
    {
        bgColor.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        if (MainGameState.gameState.myTeam == ship.team)
        {
            PersonnelListItem2 personnelListItem = pointerDrag.GetComponent<PersonnelListItem2>();
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
        if (MainGameState.gameState.myTeam == ship.team)
        {
            PersonnelListItem2 personnelListItem = pointerDrag.GetComponent<PersonnelListItem2>();
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
        return MainGameState.gameState.myTeam == ship.team;
    }

    protected override bool isDroppable()
    {
        return MainGameState.gameState.myTeam == ship.team;
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

    public void OnMouseUp()
    {
        if (!isDragging)
        {
            if (MainGameState.gameState.myTeam == ship.team)
            {
                showShipContentsEvent.Invoke(ship);
            }
        }
    }
}
