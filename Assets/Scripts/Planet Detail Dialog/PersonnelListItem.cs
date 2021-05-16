using UnityEngine;
using UnityEngine.EventSystems;

public class PersonnelListItem : MonoBehaviour,
    IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public SpriteRenderer personnelImg;

    private Personnel personnel;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas; // For drag-n-drop scale factor

    // dragging state
    private bool isValidDrop = false;
    private Vector2 origin;
    private float originalZorder;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
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


    public void setCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag " + personnel.type.name);
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
        //Debug.Log("OnDrag " + personnel.type.name);
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        Vector3 locPos = gameObject.transform.localPosition;
        locPos.z = originalZorder;
        gameObject.transform.localPosition = locPos;
        if (isValidDrop)
        {
            Debug.Log("VALID!");
        } else
        {
            Debug.Log("INvalid - reset position now");
            rectTransform.anchoredPosition = origin;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown " + personnel.type.name);
    }

    public void setIsValidDrop(bool isValidDrop)
    {
        this.isValidDrop = isValidDrop;
    }
}
