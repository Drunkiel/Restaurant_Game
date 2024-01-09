using System.Collections.Generic;
using UnityEngine;

public class MultiTriggerController : MonoBehaviour
{
    public bool isTriggered;
    public string[] objectsTag;
    public HashSet<string> objectsTagsSet;

    void Start()
    {
        objectsTagsSet = new HashSet<string>(objectsTag);
    }

    void OnTriggerEnter(Collider collider)
    {
        CheckCollision(collider);
    }

    void OnTriggerStay(Collider collider)
    {
        CheckCollision(collider);
    }

    void OnTriggerExit(Collider collider)
    {
        CheckCollision(collider, false);
    }

    void CheckCollision(Collider collider, bool enter = true)
    {
        string colliderTag = collider.tag;

        if (objectsTagsSet.Contains(colliderTag))
        {
            isTriggered = enter;
            return;
        }
    }
}
