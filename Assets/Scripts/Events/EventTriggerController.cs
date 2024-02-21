using UnityEngine;
using UnityEngine.Events;

public class EventTriggerController : MonoBehaviour
{
    public string objectTag;

    public UnityEvent enterEvent;
    public UnityEvent stayEvent;
    public UnityEvent exitEvent;

    void OnTriggerEnter(Collider collider)
    {
        CheckCollision(collider, enterEvent);
    }

    void OnTriggerStay(Collider collider)
    {
        CheckCollision(collider, stayEvent);
    }

    void OnTriggerExit(Collider collider)
    {
        CheckCollision(collider, exitEvent);
    }

    void CheckCollision(Collider collider, UnityEvent events)
    {
        if (objectTag.Contains(collider.tag))
        {
            events.Invoke();
            return;
        }
    }
}
