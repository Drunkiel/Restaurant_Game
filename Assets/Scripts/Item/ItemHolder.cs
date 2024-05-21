using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder instance;

    public bool isHoldingItem;
    public bool isHoldingStackableItem;
    public static readonly int maxItemsStack = 5;

    public ItemID holdingItem;
    public List<ItemID> holdingStackableItems = new();
    private ItemID lastPickedObject;

    public Transform holderTransform;
    public Transform itemPlaceTransform;

    private bool canDrop = true;

    private void Awake()
    {
        instance = this;
    }

    public bool PickItem(ItemID _itemID, bool destroy = true)
    {
        //Check if can be picked
        if (!_itemID.isPickable)
            return false;

        //Check if reached max capacity
        if (holdingStackableItems.Count >= maxItemsStack)
            return false;

        //Check if item is stackable
        if (_itemID.isStackable)
        {
            //Checking if is holding stackable item
            if (isHoldingStackableItem)
            {
                //Checks if stacking the same item type
                if (!CheckIfTheSameItem(_itemID))
                    return false;

                Pick(_itemID, holdingStackableItems[0].transform, holdingStackableItems.Count, false);
                holdingStackableItems.Add(lastPickedObject);
                holdingStackableItems[0]._dishItem.stackedItems.Add(lastPickedObject);

                //Adding items from the picked item
                for (int i = 1; i < holdingStackableItems.Count; i++)
                {
                    if (holdingStackableItems[i]._dishItem == null)
                        break;

                    if (holdingStackableItems[i]._dishItem.stackedItems.Count > 0)
                        holdingStackableItems.AddRange(holdingStackableItems[i]._dishItem.stackedItems);
                }

                isHoldingStackableItem = true;
                if (destroy)
                    Destroy(_itemID.gameObject);
                return true;
            }

            //Checking if is holding a non stacking item
            if (isHoldingItem)
            {
                if (_itemID._dishItem.stackedItems.Count >= maxItemsStack - 1)
                    return false;

                //Spawn stackable item
                Pick(_itemID, holderTransform, holdingStackableItems.Count);
                holdingStackableItems.Add(lastPickedObject);
                if (destroy)
                    Destroy(_itemID.gameObject);
                isHoldingStackableItem = true;

                //Then recreate holding item and reset position
                holdingItem.transform.localScale = Vector3.one;
                Pick(holdingItem, holdingStackableItems[0].transform, holdingStackableItems.Count, false);
                holdingStackableItems[0]._dishItem.stackedItems.Add(lastPickedObject);
                holdingStackableItems.AddRange(holdingStackableItems[0]._dishItem.stackedItems);
                lastPickedObject.transform.localPosition = new Vector3(
                    0,
                    holdingStackableItems[^1].transform.localPosition.y,
                    0);
                Destroy(holdingItem.gameObject);
                isHoldingItem = false;
                return true;
            }

            //If dont hold anything, pick it
            Pick(_itemID, holderTransform, holdingStackableItems.Count);
            holdingStackableItems.Add(lastPickedObject);
            holdingStackableItems.AddRange(lastPickedObject._dishItem.stackedItems);
            isHoldingStackableItem = true;
            if (destroy)
                Destroy(_itemID.gameObject);
            return true;
        }
        else //If item is not stackable
        {
            //If holds a not stackable item: stop script
            if (isHoldingItem)
                return false;

            //Checks if is holding stackable item like plate to add it to it
            if (isHoldingStackableItem)
            {
                //Checks if stacking the same type
                if (!CheckIfTheSameItem(_itemID))
                    return false;

                Pick(_itemID, holdingStackableItems[0].transform, holdingStackableItems.Count, false);
                holdingStackableItems.Add(lastPickedObject);
                holdingStackableItems[0]._dishItem.stackedItems.Add(lastPickedObject);
                if (destroy)
                    Destroy(_itemID.gameObject);
                return true;
            }
            else if (holdingStackableItems.Count <= 1) //if not just pick it
            {
                Pick(_itemID, itemPlaceTransform, holdingStackableItems.Count);
                holdingItem = lastPickedObject;
                isHoldingItem = true;
                if (destroy)
                    Destroy(_itemID.gameObject);
                return true;
            }
        }

        return false;
    }

    public void Pick(ItemID _itemID, Transform transform, int multiplier, bool shrinkObject = true, bool deactivateInteractions = true)
    {
        GameObject newItem = Instantiate(_itemID.gameObject, transform);
        if (shrinkObject) newItem.transform.localScale /= 100;
        newItem.transform.SetLocalPositionAndRotation(new Vector3(0, _itemID.heightPlacement * multiplier, 0), _itemID.transform.rotation);
        newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        newItem.name = _itemID.gameObject.name;
        if (deactivateInteractions)
            newItem.transform.GetChild(1).gameObject.SetActive(false);
        lastPickedObject = newItem.GetComponent<ItemID>();
    }

    public void DropItemAction(InputAction.CallbackContext context)
    {
        if (context.performed && canDrop)
            DropItem();
    }

    private void DropItem(bool freezeRotation = false)
    {
        if (isHoldingItem)
        {
            itemPlaceTransform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            Vector3 spawnPosition = transform.position + transform.forward * 1 / 4;
            GameObject newItem = Instantiate(holdingItem.gameObject, new Vector3(spawnPosition.x, itemPlaceTransform.position.y, spawnPosition.z), holdingItem.transform.rotation);
            newItem.transform.localScale *= 100;
            newItem.name = holdingItem.name;

            //Unfreezing position
            newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            if (freezeRotation)
                newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            canDrop = false;
            Invoke(nameof(ResetDrop), 1f);

            Destroy(holdingItem.gameObject);
            isHoldingItem = false;
            return;
        }

        if (isHoldingStackableItem)
        {
            holderTransform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            Vector3 spawnPosition = transform.position + transform.forward * 1 / 4;
            GameObject newItem = Instantiate(holdingStackableItems[0].gameObject, new Vector3(spawnPosition.x, holderTransform.position.y, spawnPosition.z), holdingStackableItems[0].transform.rotation);
            newItem.transform.localScale *= 100;
            newItem.name = holdingStackableItems[0].name;

            //Unfreezing position
            newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            Destroy(holdingStackableItems[0].gameObject);

            canDrop = false;
            Invoke(nameof(ResetDrop), 1f);

            holdingStackableItems.Clear();
            isHoldingStackableItem = false;
            return;
        }
    }

    private bool CheckIfTheSameItem(ItemID _itemID)
    {
        if (holdingStackableItems.Count >= 2 &&
            ((holdingStackableItems[1]._dishItem == null && _itemID._dishItem != null) ||
            (holdingStackableItems[1]._dishItem != null && _itemID._dishItem == null)))
            return false;
        else
            return true;
    }

    private void ResetDrop()
    {
        canDrop = true;
    }
}
