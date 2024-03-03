using UnityEngine;

public class OrderAction : MonoBehaviour
{
    public OrderData _currentOrder;
    private ActionController _actionController;
    private Animator anim;

    private void Start()
    {
        _actionController = GetComponent<ActionController>();
        anim = GetComponent<Animator>();
    }

    public void MakeOrder()
    {
        OrderController.instance.NewOrder(GetComponent<ItemID>(), this);
        _actionController.EndAction();
    }

    public void EatMeal()
    {
        if (!anim.GetBool("isEating"))
            Invoke(nameof(FinishMeal), 5f);

        anim.SetBool("isEating", true);
    }

    private void FinishMeal()
    {
        anim.SetBool("isEating", false);
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
