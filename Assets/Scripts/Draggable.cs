using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Draggable: MonoBehaviour,
    IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector2 origin;
    private float originalZorder;
    private bool isValidDrop = false;

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
        isValidDrop = false;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        origin = rectTransform.anchoredPosition;
        Vector3 locPos = gameObject.transform.localPosition;
        originalZorder = locPos.z;
        locPos.z = -99;
        gameObject.transform.localPosition = locPos;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
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
