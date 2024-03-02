using UnityEngine;

public class OrderAction : MonoBehaviour
{
    public OrderData _currentOrder;
    private ActionController _actionController;

    private void Start()
    {
        _actionController = GetComponent<ActionController>();
    }

    public void MakeOrder()
    {
        OrderController.instance.NewOrder(GetComponent<ItemID>(), this);
        _actionController.EndAction();
    }

    public void CheckForOrder()
    {
        PlaceItemInteraction _itemInteraction = GetComponent<SitAction>()._seatID.transform.parent.GetChild(0).GetComponent<PlaceItemInteraction>();
        ItemID _itemID = _itemInteraction.HoldingItem();

        if (_itemID != null && _itemID.itemID.Equals(_currentOrder._itemID.itemID))
            _actionController.EndAction();
    }
}
