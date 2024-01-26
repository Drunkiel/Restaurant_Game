using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Dish,
}

public class ItemID : MonoBehaviour
{
    public string itemName;
    public bool isStackable;
    public ItemType itemType;
    public List<ItemID> stackedItems = new();
}
