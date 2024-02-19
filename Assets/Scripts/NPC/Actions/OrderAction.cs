using UnityEngine;

public class OrderAction : MonoBehaviour
{
    private ActionController _actionController;

    private void Start()
    {
        _actionController = GetComponent<ActionController>();
    }

    public void MakeOrder()
    {
        OrderController.instance.NewOrder(GetComponent<ItemID>());
        _actionController.EndAction();
    }
}
