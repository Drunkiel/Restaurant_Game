using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleOrder
{
    public OrderData _order;
    public ItemID _NPCID;
    public BuildingCard _card;
}

public class OrderController : MonoBehaviour
{
    public static OrderController instance;

    [SerializeField] private OrderData[] _possibleOrders;
    public List<SingleOrder> _ordersToDo = new();
    private int allDoneOrders;

    [SerializeField] private Transform parent;
    [SerializeField] private GameObject cardPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //TEST ONLY
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) 
            NewOrder(new ItemID(), new OrderAction());
    }

    public void NewOrder(ItemID _itemID, OrderAction _orderAction)
    {
        GameObject newCard = Instantiate(cardPrefab, parent);

        SingleOrder _newOrder = new()
        {
            _order = _possibleOrders[Random.Range(0, _possibleOrders.Length)],
            _NPCID = _itemID,
            _card = newCard.GetComponent<BuildingCard>()
        };

        _newOrder._card.SetCardData(_newOrder._order.sprite, $"{_newOrder._order.price}$ - {_newOrder._NPCID.itemName}");
        _orderAction._currentOrder = _newOrder._order;
        _ordersToDo.Add(_newOrder);
    }
}
