using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Order/Order data")]
public class OrderData : ScriptableObject
{
    public int price;
    public Sprite sprite;
    public ItemID itemID;
}
