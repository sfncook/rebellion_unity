using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DragAndDropable : MonoBehaviour,
    IBeginDragHandler, IEndDragHandler, IDragHandler,
    IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector2 origin;
    private float originalZorder;
    private bool isValidDrop = false;

    protected bool isDraggable = true;
    protected bool isDroppable = true;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void setCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }

    public void setIsValidDrop(bool isValidDrop)
    {
        this.isValidDrop = isValidDrop;
    }



    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if(isDraggable)
        {
            isValidDrop = false;
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            origin = rectTransform.anchoredPosition;
            Vector3 locPos = gameObject.transform.localPosition;
            originalZorder = locPos.z;
            locPos.z = -99;
            gameObject.transform.localPosition = locPos;
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;
            Vector3 locPos = gameObject.transform.localPosition;
            locPos.z = originalZorder;
            gameObject.transform.localPosition = locPos;
            if (isValidDrop)
            {
                Debug.Log("VALID!");
            }
            else
            {
                Debug.Log("INvalid - reset position now");
                rectTransform.anchoredPosition = origin;
            }
        }
    }



    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        //PersonnelListItem personnelListItem = eventData.pointerDrag.GetComponent<PersonnelListItem>();
        //if (
        //    personnelListItem != null &&
        //    personnelListItem.getPersonnel().team == ship.team &&
        //    ship.personnelsOnBoard.Count < ((ShipType)ship.type).personnelCapacity
        //    )
        //{
        //    personnelListItem.setIsValidDrop(true);
        //    ship.personnelsOnBoard.Add(personnelListItem.getPersonnel());
        //    removePersonnelCallback(personnelListItem.getPersonnel());
        //}
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //if (eventData.dragging)
        //{
        //    bgColor.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        //}
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //if (eventData.dragging)
        //{
        //    PersonnelListItem personnelListItem = eventData.pointerDrag.GetComponent<PersonnelListItem>();
        //    if (
        //        personnelListItem != null &&
        //        personnelListItem.getPersonnel().team == ship.team &&
        //        ship.personnelsOnBoard.Count < ((ShipType)ship.type).personnelCapacity
        //        )
        //    {
        //        bgColor.color = new Color(1.0f, 1.0f, 0.5f, 0.6f);
        //    }
        //}
    }
}
