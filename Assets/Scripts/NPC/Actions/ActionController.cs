using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ThingsToAchieve
{
    public UnityEvent actions;
    public float distance = 0.1f;
    public bool isActionDone;
}

public class ActionController : MonoBehaviour
{
    public List<ThingsToAchieve> thingsToAchieve = new();
    [SerializeField] private bool finishedAllActions;
    public int actionIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (thingsToAchieve.Count == 0)
        {
            Debug.LogWarning($"NPC: {GetComponent<ItemID>().itemName} has no action to perform");
            return;
        }

        if (!finishedAllActions)
            thingsToAchieve[actionIndex].actions.Invoke();
    }

    public void NextAction(int amount = 1)
    {
        actionIndex += amount;

        if (thingsToAchieve.Count <= actionIndex)
            finishedAllActions = true;
        else
            thingsToAchieve[actionIndex].actions.Invoke();
    }

    public void SkipAction(int amount = 1)
    {
        //This is only for esthetics reasons
        for (int i = 0; i < amount; i++)
            thingsToAchieve[actionIndex + i].isActionDone = true;

        NextAction(amount);
    }

    public void EndAction()
    {
        thingsToAchieve[actionIndex].isActionDone = true;
        NextAction();
    }
}
