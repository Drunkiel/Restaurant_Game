using UnityEngine;

public class ProgressMetricController : MonoBehaviour
{
    public static ProgressMetricController instance;
    public MoneyManager _moneyManager;
    public OrdersManager _ordersManager;

    private void Awake()
    {
        instance = this;
    }
}
