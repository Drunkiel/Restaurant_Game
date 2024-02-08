using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder instance;

    public bool isHoldingItem;
    public bool isHoldingStackableItem;
    private static readonly int maxItemsStack = 5;

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

    private void Update()
    {
        if (isHoldingItem || isHoldingStackableItem)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (canDrop) DropItem();
            }
        }
    }

    public void PickItem(ItemID _itemID, bool destroy = true)
    {
        if (!_itemID.isPickable) return;
        if (!_itemID.isStackable)
        {
            //Holding single item
            if (!isHoldingItem)
            {
                //Checks if is holding stackable item like plate to add it to it
                if (isHoldingStackableItem && holdingStackableItems.Count < maxItemsStack)
                {
                    //Checks if stacking the same type
                    if (holdingStackableItems.Count >= 2 && !holdingStackableItems[1].itemType.Equals(_itemID.itemType)) return;

                    Pick(_itemID, holdingStackableItems[0].transform, holdingStackableItems.Count, false);
                    holdingStackableItems.Add(lastPickedObject);
                    holdingStackableItems[0].stackedItems.Add(lastPickedObject);
                    if (destroy) Destroy(_itemID.gameObject);
                    return;
                }
                else if (holdingStackableItems.Count <= 1) //if not just pick it
                {
                    Pick(_itemID, itemPlaceTransform, holdingStackableItems.Count);
                    holdingItem = lastPickedObject;
                    isHoldingItem = true;
                    if (destroy) Destroy(_itemID.gameObject);
                    return;
                }
            }
        }
        else
        {
            //Checks if holding stackable item to add it to it
            if (isHoldingStackableItem && holdingStackableItems.Count < maxItemsStack)
            {
                if (holdingStackableItems.Count >= 2 && !holdingStackableItems[1].itemType.Equals(_itemID.itemType)) return;

                Pick(_itemID, holdingStackableItems[0].transform, holdingStackableItems.Count, false);
                holdingStackableItems.Add(lastPickedObject);
                holdingStackableItems[0].stackedItems.Add(lastPickedObject);
                isHoldingStackableItem = true;
                if (destroy) Destroy(_itemID.gameObject);
                return;
            }
            else
            {
                //Checking if is holding no stackable item then recreates it 
                if (isHoldingItem)
                {
                    if (_itemID.stackedItems.Count >= maxItemsStack - 1) return;

                    //Spawn stackable item
                    Pick(_itemID, holderTransform, holdingStackableItems.Count);
                    holdingStackableItems.Add(lastPickedObject);
                    if (destroy) Destroy(_itemID.gameObject);
                    isHoldingStackableItem = true;

                    //Then recreate holding item and reset position
                    holdingItem.transform.localScale = Vector3.one;
                    Pick(holdingItem, holdingStackableItems[0].transform, holdingStackableItems.Count, false);
                    holdingStackableItems[0].stackedItems.Add(lastPickedObject);
                    holdingStackableItems.AddRange(holdingStackableItems[0].stackedItems);
                    lastPickedObject.transform.localPosition = new Vector3(
                        0,
                        holdingStackableItems[^1].transform.localPosition.y + (holdingStackableItems.Count > 1 ? holdingItem.heightPlacement : 0),
                        0);
                    if (destroy) Destroy(holdingItem.gameObject);
                    isHoldingItem = false;
                    return;
                }
                else //if not just pick item
                {
                    Pick(_itemID, holderTransform, holdingStackableItems.Count);
                    holdingStackableItems.Add(lastPickedObject);
                    holdingStackableItems.AddRange(lastPickedObject.stackedItems);
                    isHoldingStackableItem = true;
                    if (destroy) Destroy(_itemID.gameObject);
                    return;
                }
            }
        }
    }

    public void Pick(ItemID _itemID, Transform transform, int multiplier, bool shrinkObject = true, bool deactivateInteractions = true)
    {
        GameObject newItem = Instantiate(_itemID.gameObject, transform);
        if (shrinkObject) newItem.transform.localScale /= 100;
        newItem.transform.SetLocalPositionAndRotation(new Vector3(0, _itemID.heightPlacement * multiplier, 0), _itemID.transform.rotation);
        newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        newItem.name = _itemID.gameObject.name;
        if(deactivateInteractions) newItem.transform.GetChild(1).gameObject.SetActive(false);
        lastPickedObject = newItem.GetComponent<ItemID>();
    }

    public void DropItem(bool freezeRotation = false)
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
            if (freezeRotation) newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            canDrop = false;
            Invoke(nameof(ResetDrop), 2f);

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

    private void ResetDrop()
    {
        canDrop = true;
    }
}
