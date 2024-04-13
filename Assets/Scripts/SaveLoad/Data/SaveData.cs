using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int money;
    public int currentRating;
    public int finishedOrders;
    public List<int> restaurantMenu = new();
}