using UnityEngine;
using UnityEngine.UI;

public class EquipController : MonoBehaviour
{
    public OrderData _data;

    [SerializeField] private Toggle toggle;
    [SerializeField] private int maxEquipedOrders = 5;

    public void UpdateToggle()
    {
        switch (toggle.isOn)
        {
            case true:

                break;

            case false:
                Activate();
                break;
        }
    }

    private void Activate()
    {
        OrderController _orderController = OrderController.instance;

        if (_orderController._restaurantMenu.Count < maxEquipedOrders)
            _orderController._restaurantMenu.Add(_data);
        else
            toggle.isOn = false;
    }
}
