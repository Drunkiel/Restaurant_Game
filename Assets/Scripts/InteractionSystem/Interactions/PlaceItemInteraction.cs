using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItemInteraction : MonoBehaviour
{
    [SerializeField] private bool isHoldingStackableItem;
    [SerializeField] private bool pickAll;
    [SerializeField] private Transform itemHolder;
    [HideInInspector] public bool isMaking;
    public List<ItemID> holdingItems = new();

    private InteractableObject _interactableObject;
    private ItemHolder _itemHolder;

    private HintEvent _hintEvent;

    private void Start()
    {
        _interactableObject = GetComponent<InteractableObject>();
        _itemHolder = ItemHolder.instance;

        if (TryGetComponent(out HintEvent _hintEvent))
            this._hintEvent = _hintEvent;
    }

    private void Update()
    {
        if (_hintEvent == null) 
            return;

        if (!_itemHolder.isHoldingItem && !_itemHolder.isHoldingStackableItem && !isHoldingStackableItem)
            _hintEvent.GetComponent<EventTriggerController>().canBeShown = false;
        else
            _hintEvent.GetComponent<EventTriggerController>().canBeShown = true;

        if (_interactableObject.isPlayerNearby)
            _hintEvent.addOne = _itemHolder.isHoldingItem || (_itemHolder.isHoldingStackableItem && !isHoldingStackableItem);
    }

    public void PlaceItem()
    {
        //Checking if player is interacting
        if (InteractionSystem.isInteracting || isMaking)
            return;

        if (_itemHolder.isHoldingItem)
        {
            if (isHoldingStackableItem)
                AddToHolder();
            return;
        }

        if (holdingItems.Count > 0)
        {
            if (!_itemHolder.isHoldingItem || _itemHolder.isHoldingStackableItem)
            {
                PlaceOnPlayer();
                return;
            }
        }

        if (!isHoldingStackableItem && _itemHolder.isHoldingStackableItem)
        {
            PlaceOnHolder();
            return;
        }
    }

    private void PlaceOnHolder()
    {
        _itemHolder.Pick(_itemHolder.holdingStackableItems[0], itemHolder, holdingItems.Count, false);
        ItemID placedObject = itemHolder.GetChild(0).GetComponent<ItemID>();
        holdingItems.Add(placedObject);
        holdingItems.AddRange(placedObject._dishItem.stackedItems);
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
        if (holdingItems.Count > 1)
            if (holdingItems[^1].isStackable)
                return;

        _itemHolder.Pick(_itemHolder.holdingItem, holdingItems[0].transform, holdingItems.Count, false);
        ItemID placedObject = holdingItems[0].transform.GetChild(holdingItems[0].transform.childCount - 1).GetComponent<ItemID>();
        holdingItems.Add(placedObject);
        holdingItems[0]._dishItem.stackedItems.Add(placedObject);
        placedObject.transform.localScale = Vector3.one;
        _itemHolder.isHoldingItem = false;
        Destroy(_itemHolder.holdingItem.gameObject);

        return;
    }

    private void PlaceOnPlayer()
    {
        if (holdingItems.Count >= 1 && !pickAll)
        {
            _itemHolder.PickItem(holdingItems[^1]);
            if (holdingItems.Count > 1)
                holdingItems[0]._dishItem.stackedItems.RemoveAt(holdingItems[0]._dishItem.stackedItems.Count - 1);
            holdingItems.RemoveAt(holdingItems.Count - 1);

            if (holdingItems.Count == 0)
                isHoldingStackableItem = false;
        }
        else if (_itemHolder.holdingStackableItems.Count <= 1)
        {
            if (!holdingItems[0].isPickable) 
                return;
            _itemHolder.PickItem(holdingItems[0]);
            holdingItems = new();
            StartCoroutine(nameof(ResetBool));
        }

        return;
    }

    IEnumerator ResetBool()
    {
        yield return new WaitForSeconds(0.1f);
        isHoldingStackableItem = false;
    }

    public ItemID GetHoldingItem()
    {
        if (holdingItems.Count < 2) 
            return null;

        return holdingItems[1];
    }

    public ItemID GetDishItem()
    {
        if (holdingItems.Count < 1)
            return null;

        return holdingItems[0];
    }
}
