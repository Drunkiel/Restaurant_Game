using UnityEngine;

public class OrderControlPanel : MonoBehaviour
{
    public DisplayController _displayController;
    public PriceController _priceController;
    public EquipController _equipController;
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

    public void ManageData(OrderCard _orderCard)
    {
        UI.SetActive(true);
        _displayController.UpdateDisplay(_orderCard._data.sprite, _orderCard._data._itemID.itemName);

        _priceController._data = _orderCard._data;
        _priceController.SetData();

        _equipController._orderCard = _orderCard;
        _equipController.toggle.isOn = _orderCard.equippedImage.gameObject.activeSelf;
        _equipController.UpdateToggle();
    }
}
