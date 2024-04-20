using UnityEngine;

public class ItemID : MonoBehaviour
{
    public short itemID;
    public string itemName;
    public Sprite itemSprite;
    public bool isPickable = true;
    public bool isStackable = true;
    public float heightPlacement = 0.05f;
#nullable enable
    public DishItem? _dishItem;
}
