using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Shop/Item data")]
public class ShopItemData : ScriptableObject
{
    public int price;
    public ItemID _itemID;
}
