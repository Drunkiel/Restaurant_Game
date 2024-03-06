using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleOrder
{
    public int orderIndex;
    public OrderData _orderData;
    public ItemID _NPCID;
    public BuildingCard _card;
}

public class OrderController : MonoBehaviour
{
    public static OrderController instance;

    [SerializeField] private OrderData[] _possibleOrders;
    public List<SingleOrder> _ordersToDo = new();
    public int finishedOrders;

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
            _orderData = _possibleOrders[Random.Range(0, _possibleOrders.Length)],
            _NPCID = _itemID,
            _card = newCard.GetComponent<BuildingCard>()
        };

        _newOrder._card.SetCardData(_newOrder._orderData.sprite, $"{_newOrder._orderData.price}$ - {_newOrder._NPCID.itemName}");
        _orderAction._currentOrder = _newOrder;
        _ordersToDo.Add(_newOrder);
    }

    public void DestroyOrders()
    {
        finishedOrders = 0;
        _ordersToDo.Clear();
    }
}
