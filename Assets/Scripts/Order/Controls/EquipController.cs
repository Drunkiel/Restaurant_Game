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

        if (_orderController._restaurantMenu.Contains(_orderCard._data))
            return;

        if (_orderController._restaurantMenu.Count < maxEquipedOrders)
        {
            _orderController._restaurantMenu.Add(_orderCard._data);
            _orderCard.UpdateEquipImage(true);
        }
        else
            toggle.isOn = false;
    }

    private void Deactivate()
    {
        OrderController _orderController = OrderController.instance;
        
        _orderController._restaurantMenu.Remove(_orderCard._data);
        _orderCard.UpdateEquipImage(false);
    }
}
