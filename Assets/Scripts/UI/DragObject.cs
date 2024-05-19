using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler
{
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
}
