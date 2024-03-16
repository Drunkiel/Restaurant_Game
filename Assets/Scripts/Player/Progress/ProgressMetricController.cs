using UnityEngine;

public class ProgressMetricController : MonoBehaviour
{
    public static ProgressMetricController instance;
    public MoneyManager _moneyManager;

    private void Awake()
    {
        instance = this;
    }
}
