using System.Collections.Generic;
using UnityEngine;

public class DishItem : MonoBehaviour
{
    public MakingProcess process;
    public int maxStackCount = 5;
    public List<ItemID> stackedItems = new();
}
