using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder instance;

    public bool isHoldingItem;
    public bool isHoldingStackableItem;
    private readonly int maxItemsStack = 5;

    [SerializeField] private ItemID holdingItem;
    [SerializeField] private List<ItemID> holdingStackableItems = new();
    private ItemID lastPickedObject;

    [SerializeField] private Transform holderTransform;
    [SerializeField] private Transform itemPlaceTransform;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isHoldingItem)
        {
            if (Input.GetKeyDown(KeyCode.Q)) DropItem();
        }
    }

    public void PickItem(ItemID _itemID)
    {
        if (!_itemID.isStackable)
        {
            if (!isHoldingItem)
            {
                if (isHoldingStackableItem && holdingStackableItems.Count < maxItemsStack)
                {
                    Pick(_itemID, holdingStackableItems[0].transform, false);
                    holdingStackableItems.Add(lastPickedObject);
                    Destroy(_itemID.gameObject);
                    return;
                }
                else if (holdingStackableItems.Count <= 1)
                {
                    Pick(_itemID, itemPlaceTransform);
                    holdingItem = lastPickedObject;
                    isHoldingItem = true;
                    Destroy(_itemID.gameObject);
                    return;
                }
            }
        }
        else
        {
            if (isHoldingStackableItem && holdingStackableItems.Count < maxItemsStack)
            {
                Pick(_itemID, holdingStackableItems[0].transform, false);
                holdingStackableItems.Add(lastPickedObject);
                isHoldingStackableItem = true;
                Destroy(_itemID.gameObject);
                return;
            }
            else
            {
                if (isHoldingItem)
                {
                    //Spawn stackable item
                    Pick(_itemID, holderTransform);
                    holdingStackableItems.Add(lastPickedObject);
                    Destroy(_itemID.gameObject);
                    isHoldingStackableItem = true;

                    //Then recreate holding item
                    holdingItem.transform.localScale = Vector3.one; 
                    Pick(holdingItem, holdingStackableItems[0].transform, false);
                    holdingStackableItems.Add(lastPickedObject);
                    Destroy(holdingItem.gameObject);
                    isHoldingItem = false;
                    return;
                }
                else
                {
                    Pick(_itemID, holderTransform);
                    holdingStackableItems.Add(lastPickedObject);
                    isHoldingStackableItem = true;
                    Destroy(_itemID.gameObject);
                    return;
                }
            }
        }
    }

    private void Pick(ItemID _itemID, Transform transform, bool shrinkObject = true)
    {
        GameObject newItem = Instantiate(_itemID.gameObject, transform);
        if (shrinkObject) newItem.transform.localScale /= 100;
        newItem.transform.SetLocalPositionAndRotation(Vector3.zero + new Vector3(0, 0.05f * holdingStackableItems.Count, 0), _itemID.transform.rotation);
        newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        newItem.name = _itemID.gameObject.name;
        newItem.transform.GetChild(1).gameObject.SetActive(false);
        lastPickedObject = newItem.GetComponent<ItemID>();
    }

    public void DropItem()
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

            Destroy(holdingItem.gameObject);
            isHoldingItem = false;
            return;
        }

        if (isHoldingStackableItem)
        {

        }
    }
}
