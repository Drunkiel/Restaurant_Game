using UnityEngine;
using UnityEngine.UI;

public class EquipController : MonoBehaviour
{
    public OrderCard _orderCard;

    public Toggle toggle;
    [SerializeField] private int maxEquipedOrders = 5;

    public void UpdateToggle()
    {
        switch (toggle.isOn)
        {
            case true:
                Activate();
                break;

            case false:
                Deactivate();
                break;
        }
    }

    private void Activate()
    {
        OrderController _orderController = OrderController.instance;
        int orderIndex = _orderController._possibleOrders.IndexOf(_orderCard._data);

        if (_orderController._restaurantMenu.Contains(orderIndex))
            return;

        if (_orderController._restaurantMenu.Count < maxEquipedOrders)
        {
            _orderController._restaurantMenu.Add(orderIndex);
            _orderCard.UpdateEquipImage(true);
        }
        else
            toggle.isOn = false;
    }

    private void Deactivate()
    {
        OrderController _orderController = OrderController.instance;
        int orderIndex = _orderController._possibleOrders.IndexOf(_orderCard._data);

        _orderController._restaurantMenu.Remove(orderIndex);
        _orderCard.UpdateEquipImage(false);
    }
}
