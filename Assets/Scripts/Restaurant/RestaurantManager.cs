using System.Collections.Generic;
using UnityEngine;

public class RestaurantManager : MonoBehaviour
{
    public static RestaurantManager instance;

    public List<ItemID> allSits = new();

    private void Awake()
    {
        instance = this;
    }

    public int LookForAvailableSit()
    {
        for (int i = 0; i < allSits.Count; i++)
        {
            if (allSits[i].stackedItems.Count <= 0) return i;
        }

        return -1;
    }
}
