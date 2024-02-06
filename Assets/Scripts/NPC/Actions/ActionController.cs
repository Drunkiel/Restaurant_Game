using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ThingsToAchieve
{
    public UnityEvent actions;
    public bool isActionDone;
}

public class ActionController : MonoBehaviour
{
    public List<ThingsToAchieve> thingsToAchieve = new();
    private bool finishedAllActions;
    private int lastThing = 0;

    // Update is called once per frame
    void Update()
    {
        if (!finishedAllActions)
            thingsToAchieve[lastThing].actions.Invoke();
    }

    public void EndAction()
    {
        lastThing += 1;
        if (thingsToAchieve.Count == lastThing) finishedAllActions = true;
        else thingsToAchieve[lastThing].actions.Invoke();
    }
}
