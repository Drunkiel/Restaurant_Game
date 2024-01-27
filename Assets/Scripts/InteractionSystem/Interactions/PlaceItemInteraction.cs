using UnityEngine;

public class PlaceItemInteraction : MonoBehaviour
{
    [SerializeField] private bool isHoldingItem;
    [SerializeField] private bool isHoldingStackableItem;
    [SerializeField] private Transform itemHolder;

    public void PlaceItem()
    {
        if (isHoldingItem) PlaceOnPlayer();

        /*if (ItemHolder.instance.isHoldingItem && !isHoldingStackableItem) return;*/

        if (!isHoldingItem) PlaceOnHolder();
    }

    private void PlaceOnHolder()
    {
        ItemHolder.instance.holdingStackableItems[0].transform.parent = itemHolder;
        itemHolder.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        isHoldingItem = true;
        return;
    }

    private void PlaceOnPlayer()
    {
        ItemHolder.instance.PickItem(itemHolder.GetChild(0).gameObject.GetComponent<ItemID>());
        GameObject a = itemHolder.GetChild(0).gameObject;
        a.transform.parent = ItemHolder.instance.holderTransform;
        a.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        isHoldingItem = false;
        return;
    }
}
