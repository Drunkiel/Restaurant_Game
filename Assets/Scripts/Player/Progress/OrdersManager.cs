[System.Serializable]
public class OrdersManager
{
    public int finishedOrders;

    public void IncreaseFinishedOrdersCounter()
    {
        finishedOrders++;
        OrderController.instance.finishedOrders += 1;
        SummaryController.instance.satisfiedCostomers += 1;
    }
}
