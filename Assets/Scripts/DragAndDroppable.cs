using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public abstract class DragAndDroppable : MonoBehaviour,
    IBeginDragHandler, IEndDragHandler, IDragHandler,
    IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector2 origin;
    private float originalZorder;
    private bool isValidDrop = false;

    protected abstract bool isDraggable();
    protected abstract bool isDroppable();

    protected virtual List<string> acceptedDropTypes()
    {
        // Override as-needed
        return new List<string>() {};
    }
    protected virtual void onDrop(GameObject pointerDrag)
    {
        // Override as-needed
    }
    protected virtual void onPointEnter(GameObject pointerDrag)
    {
        // Override as-needed
    }
    protected virtual void onPointExit()
    {
        // Override as-needed
    }




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
        if (isDraggable())
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
        if (isDraggable())
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable())
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;
            Vector3 locPos = gameObject.transform.localPosition;
            locPos.z = originalZorder;
            gameObject.transform.localPosition = locPos;
            if (!isValidDrop)
            {
                rectTransform.anchoredPosition = origin;
            }
        }
    }



    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if(isDroppable())
        {
            GameObject dragging = getDraggingObject(eventData);
            if (dragging != null)
            {
                onDrop(dragging);
            }
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (isDroppable())
        {
            if (eventData.dragging)
            {
                onPointExit();
            }
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (isDroppable())
        {
            if (eventData.dragging)
            {
                GameObject dragging = getDraggingObject(eventData);
                if (dragging != null)
                {
                    onPointEnter(dragging);
                }
            }
        }
    }

    private GameObject getDraggingObject(PointerEventData eventData)
    {
        if (isDroppable())
        {
            //Debug.Log("isDroppable OnPointerEnter eventData:" + eventData.pointerDrag+ "  acceptedDropTypes:"+ acceptedDropTypes()[0]);
            foreach (string type in acceptedDropTypes())
            {
                Component component = eventData.pointerDrag.GetComponent(type);
                if (component != null)
                {
                    return component.gameObject;
                }
            }
        }
        return null;
    }
}
