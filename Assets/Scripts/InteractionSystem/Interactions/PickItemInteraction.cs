using UnityEngine;

public class PickItemInteraction : MonoBehaviour
{
    public ItemID _objectsID;
    public bool destroy = true;

    public void PickItem()
    {
        if (TryGetComponent(out HintEvent _hintEvent))
            _hintEvent.ChangeHint(0);

        ItemHolder.instance.PickItem(_objectsID, destroy);
    }

    public bool CanPickItem()
    {
        ItemHolder _itemHolder = ItemHolder.instance;

        if (_itemHolder.holdingItem != null)
            return false;

        if (_itemHolder.holdingStackableItems.Count >= ItemHolder.maxItemsStack)
            return false;

        return true;
    }
}
