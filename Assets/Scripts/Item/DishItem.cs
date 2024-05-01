using System.Collections.Generic;
using UnityEngine;

public class DishItem : MonoBehaviour
{
    public MakingProcess process;
    public int maxStackCount = 5;
    public bool isDirty;
    public List<ItemID> stackedItems = new();

    public void ChangeDirtyState(bool state)
    {
        isDirty = state;
        transform.GetChild(0).GetChild(0).gameObject.SetActive(state);
    }
}
