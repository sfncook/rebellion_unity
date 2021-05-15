using UnityEngine;
using UnityEngine.EventSystems;

public class ShipListItem : MonoBehaviour,
    IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public SpriteRenderer shipImg;
    public SpriteRenderer hasPersonnelImg;

    private Ship ship;
    private RectTransform rectTransform;
    private Canvas canvas; // For drag-n-drop scale factor

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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
        //Debug.Log("Dropped: " + eventData.pointerDrag.GetType().Name);
        PersonnelListItem personnelListItem = eventData.pointerDrag.GetComponent<PersonnelListItem>();
        if (personnelListItem != null)
        {
            Debug.Log("Dropped personnel! " + personnelListItem.getPersonnel().type.name);
            personnelListItem.setIsValidDrop(true);
        }
        else
        {
            Debug.Log("Dropped something odd" + eventData.pointerDrag.GetType().Name);
        }
    }
}
