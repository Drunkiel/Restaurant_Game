using UnityEngine;

public class PickItemInteraction : MonoBehaviour
{
    public ItemID _objectsID;
    public bool destroy = true;

    public void PickItem()
    {
        ItemHolder.instance.PickItem(_objectsID, destroy);
    }
}
