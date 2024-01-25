using UnityEngine;

public class PickItemInteraction : MonoBehaviour
{
    public ItemID _objectsID;

    public void PickItem()
    {
        ItemHolder.instance.PickItem(_objectsID);
    }
}
