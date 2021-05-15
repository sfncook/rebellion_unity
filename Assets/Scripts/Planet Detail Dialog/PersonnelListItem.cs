using UnityEngine;
using UnityEngine.EventSystems;

public class PersonnelListItem : MonoBehaviour,
    IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public SpriteRenderer personnelImg;

    private Personnel personnel;
    private RectTransform rectTransform;
    private Canvas canvas; // For drag-n-drop scale factor

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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


    public void setCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag " + personnel.type.name);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag " + personnel.type.name);
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag " + personnel.type.name);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown " + personnel.type.name);
    }
}
