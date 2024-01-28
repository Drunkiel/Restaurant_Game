using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItemInteraction : MonoBehaviour
{
    [SerializeField] private bool isHoldingStackableItem;
    [SerializeField] private Transform itemHolder;
    public List<ItemID> holdingItems = new();

    public void PlaceItem()
    {
        ItemHolder _itemHolder = ItemHolder.instance;

        if (_itemHolder.isHoldingItem)
        {
            if (isHoldingStackableItem) AddToHolder();
            else return;
        }

        if (!_itemHolder.isHoldingItem && !_itemHolder.isHoldingStackableItem)
        {
            PlaceOnPlayer();
            StartCoroutine(nameof(ResetBool));
        }

        if (!isHoldingStackableItem) PlaceOnHolder();
    }

    private void PlaceOnHolder()
    {
        ItemHolder _itemHolder = ItemHolder.instance;

        _itemHolder.Pick(_itemHolder.holdingStackableItems[0], itemHolder, holdingItems.Count, false);
        ItemID placedObject = itemHolder.GetChild(0).GetComponent<ItemID>();
        holdingItems.Add(placedObject);
        placedObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        placedObject.transform.localScale = Vector3.one;
        _itemHolder.isHoldingStackableItem = false;
        Destroy(_itemHolder.holdingStackableItems[0].gameObject);
        _itemHolder.holdingStackableItems.Clear();

        isHoldingStackableItem = true;
        return;
    }

    private void AddToHolder()
    {
        ItemHolder _itemHolder = ItemHolder.instance;

        _itemHolder.Pick(_itemHolder.holdingItem, holdingItems[0].transform, holdingItems.Count, false);
        ItemID placedObject = holdingItems[0].transform.GetChild(holdingItems[0].transform.childCount - 1).GetComponent<ItemID>();
        holdingItems.Add(placedObject);
        placedObject.transform.localScale = Vector3.one;
        _itemHolder.isHoldingItem = false;
        Destroy(_itemHolder.holdingItem.gameObject);

        return;
    }

    private void PlaceOnPlayer()
    {
        ItemHolder _itemHolder = ItemHolder.instance;

        _itemHolder.PickItem(holdingItems[0]);
        holdingItems = new();

        return;
    }

    IEnumerator ResetBool()
    {
        yield return new WaitForSeconds(0.1f);
        isHoldingStackableItem = false;
    }
}
