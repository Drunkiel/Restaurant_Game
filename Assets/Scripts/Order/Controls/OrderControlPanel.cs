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

        for (int i = 0; i < _orderController._possibleOrders.Count; i++)
        {
            OrderData _orderData = _orderController._possibleOrders[i];
            GameObject newPrefab = Instantiate(orderCardPrefab, ordersParent);

            OrderCard _orderCard = newPrefab.GetComponent<OrderCard>();
            _orderCard._data = _orderData;
            _orderCard.SetCardData(_orderData._itemID.itemSprite, _orderData._itemID.itemName);
            _orderCard.AssignButton(_orderData._itemID);

            for (int j = 0; j < _orderController._restaurantMenu.Count; j++)
            {
                if (_orderController._restaurantMenu[j] == i)
                {
                    _orderCard.UpdateEquipImage(true);
                    break;
                }
            }
        }
    }

    public void ManageData(OrderCard _orderCard)
    {
        UI.SetActive(true);
        _displayController.UpdateDisplay(_orderCard._data._itemID.itemSprite, _orderCard._data._itemID.itemName);

        _priceController._data = _orderCard._data;
        _priceController.SetData();

        _equipController._orderCard = _orderCard;
        _equipController.toggle.isOn = _orderCard.equippedImage.gameObject.activeSelf;
        _equipController.UpdateToggle();
    }
}
