using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Order/Order data")]
public class OrderData : ScriptableObject
{
    public int price;
    public int minimalPrice;
    public int maxPrice = 5;
    public int minTimeToFinish = 5;
    public int maxTimeToFinish = 8;
    public ItemID _itemID;
    public RecipeData _recipeData;
}
