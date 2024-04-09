using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleOrder
{
    public int orderIndex;
    public ItemID _NPCID;
    public OrderCard _card;
}

public class OrderController : MonoBehaviour
{
    public static OrderController instance;

    [SerializeField] private OrderData[] _possibleOrders;
    public List<SingleOrder> _ordersToDo = new();
    public int finishedOrders;
    public int countOfOrdersToEnd;

    [SerializeField] private Transform parent;
    [SerializeField] private GameObject cardPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void NewOrder(ItemID _itemID, OrderAction _orderAction)
    {
        GameObject newCard = Instantiate(cardPrefab, parent);

        SingleOrder _newOrder = new()
        {
            orderIndex = _ordersToDo.Count,
            _NPCID = _itemID,
            _card = newCard.GetComponent<OrderCard>()
        };

        _newOrder._card._data = _possibleOrders[Random.Range(0, _possibleOrders.Length)];
        _newOrder._card.SetCardData(_newOrder._card._data.sprite, $"{_newOrder._card._data.price}$ - {_newOrder._NPCID.itemName}");
        _orderAction._currentOrder = _newOrder;
        _ordersToDo.Add(_newOrder);
    }

    public void DestroyOrders()
    {
        finishedOrders = 0;
        _ordersToDo.Clear();
    }
}
