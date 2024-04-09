using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Order/Order data")]
public class OrderData : ScriptableObject
{
    public int price;
    public int minimalPrice;
    public int maxPrice = 5;
    public Sprite sprite;
    public ItemID _itemID;
}
