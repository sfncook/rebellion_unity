using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public abstract class DragAndDroppable : MonoBehaviour,
    IBeginDragHandler, IEndDragHandler, IDragHandler,
    IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    public Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 origin;
    private float originalZorder;
    protected const float HOVER_TIMER_DUR_SEC = 0.8f;
    protected bool isHovering = false;
    protected float hoverTimerSec;
    private bool restartTimerOnDragEnd = false;


    void FixedUpdate()
    {
        if (isHovering)
        {
            if (hoverTimerSec <= 0.0f)
            {
                isHovering = false;
                onHoverTimerExpire();
            }
            else
            {
                hoverTimerSec -= Time.deltaTime;
                //Debug.Log("hoverTimerSec:" + hoverTimerSec + " Time.deltaTime:"+ Time.deltaTime);
            }
        }
    }

    protected void _FixedUpdate()
    {
        FixedUpdate();
    }

    protected virtual bool isDraggable()
    {
        // Override as-needed
        return false;
    }
    protected virtual bool isDroppable()
    {
        // Override as-needed
        return false;
    }

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
    protected virtual void onDragStart()
    {
        // Override as-needed
    }
    protected virtual void onDragStop()
    {
        // Override as-needed
    }
    protected virtual void onHoverStart()
    {
        // Override as-needed
    }
    protected virtual void onHoverStop()
    {
        // Override as-needed
    }
    protected virtual void onHoverTimerExpire()
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

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (isDraggable())
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            origin = rectTransform.anchoredPosition;
            Vector3 locPos = gameObject.transform.localPosition;
            originalZorder = locPos.z;
            locPos.z = -99;
            gameObject.transform.localPosition = locPos;
            restartTimerOnDragEnd = MainGameState.gameState.getIsTimerRunning();
            MainGameState.gameState.stopTimerEvent.Invoke();
            onDragStart();
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
        isHovering = false;
        if (isDraggable())
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;
            Vector3 locPos = gameObject.transform.localPosition;
            locPos.z = originalZorder;
            gameObject.transform.localPosition = locPos;
            rectTransform.anchoredPosition = origin;
            onDragStop();
            if(restartTimerOnDragEnd)
            {
                //MainGameState.gameState.startTimerEvent.Invoke();
            }
        }
    }



    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        isHovering = false;
        if (isDroppable())
        {
            GameObject dragging = getDraggingObject(eventData);
            if (dragging != null)
            {
                onDrop(dragging);
                DragAndDroppable draggingObj = dragging.GetComponent<DragAndDroppable>();
                draggingObj.onDragStop();
            }
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
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
                    isHovering = true;
                    hoverTimerSec = HOVER_TIMER_DUR_SEC;
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

    protected void resetHoverState()
    {
        isHovering = false;
    }
}
