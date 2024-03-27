using UnityEngine;

public class ProgressMetricController : MonoBehaviour
{
    public static ProgressMetricController instance;
    public MoneyManager _moneyManager;
    public RatingManager _ratingManager;
    public OrdersManager _ordersManager;

    private void Awake()
    {
        instance = this;
    }
}
