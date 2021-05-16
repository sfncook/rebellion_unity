using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public abstract class Droppable : MonoBehaviour, IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    protected abstract List<string> acceptedDropTypes();
    protected abstract void onDrop(GameObject pointerDrag);
    protected abstract void onPointEnter(GameObject pointerDrag);
    protected abstract void onPointExit();


    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        GameObject dragging = getDraggingObject(eventData);
        if (dragging != null)
        {
            onDrop(dragging);
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            onPointExit();
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
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

    private GameObject getDraggingObject(PointerEventData eventData)
    {
        foreach (string type in acceptedDropTypes())
        {
            Component component = eventData.pointerDrag.GetComponent(type);
            if (component != null)
            {
                return component.gameObject;
            }
        }
        return null;
    }
}
