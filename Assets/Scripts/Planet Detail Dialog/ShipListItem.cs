using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipListItem : MonoBehaviour,
    IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,
    IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    public SpriteRenderer shipImg;
    public SpriteRenderer hasPersonnelImg;
    public Image bgColor;

    private Ship ship;
    private RectTransform rectTransform;
    private Canvas canvas; // For drag-n-drop scale factor

    public delegate void RemovePersonnel(Personnel personnel);
    private RemovePersonnel removePersonnelCallback;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
    }

    public void setRemovePersonnelDelegate(RemovePersonnel removePersonnel)
    {
        this.removePersonnelCallback = removePersonnel;
    }

    public void setShip(Ship ship)
    {
        this.ship = ship;
        shipImg.sprite = Resources.Load<Sprite>("Images/Ships/" + ship.type.name);
        hasPersonnelImg.enabled = false;

        if (ship.team.Equals(Team.TeamA))
        {
            shipImg.transform.localScale = new Vector3(100, 100, 1);
            shipImg.color = Color.green;
        }
        else
        {
            shipImg.transform.localScale = new Vector3(-100, 100, 1);
            shipImg.color = Color.red;
        }
    }

    public void setCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag " + ship.type.name);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag " + ship.type.name);
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag " + ship.type.name);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown "+ship.type.name);
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        PersonnelListItem personnelListItem = eventData.pointerDrag.GetComponent<PersonnelListItem>();
        if (personnelListItem != null && personnelListItem.getPersonnel().team == ship.team)
        {
            personnelListItem.setIsValidDrop(true);
            ship.personnelsOnBoard.Add(personnelListItem.getPersonnel());
            removePersonnelCallback(personnelListItem.getPersonnel());
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            bgColor.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            PersonnelListItem personnelListItem = eventData.pointerDrag.GetComponent<PersonnelListItem>();
            if (personnelListItem != null && personnelListItem.getPersonnel().team == ship.team)
            {
                bgColor.color = new Color(1.0f, 1.0f, 0.5f, 0.6f);
            }
        }
    }
}
