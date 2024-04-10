using UnityEngine;

public class OrderControlPanel : MonoBehaviour
{
    public DisplayController _displayController;
    public PriceController _priceController;
    public GameObject UI;
    [SerializeField] private GameObject orderCardPrefab;
    [SerializeField] private Transform ordersParent;

    private void Start()
    {
        OrderController _orderController = OrderController.instance;

        foreach (OrderData _orderData in _orderController._possibleOrders)
        {
            GameObject newPrefab = Instantiate(orderCardPrefab, ordersParent);

            OrderCard _orderCard = newPrefab.GetComponent<OrderCard>();
            _orderCard.SetCardData(_orderData.sprite, _orderData._itemID.itemName);
            _orderCard._data = _orderData;
        }
    }

    public void ManageData(OrderData _data)
    {
        UI.SetActive(true);
        _displayController.UpdateDisplay(_data.sprite, _data._itemID.itemName);
        _priceController._data = _data;
        _priceController.SetData();
    }
}
