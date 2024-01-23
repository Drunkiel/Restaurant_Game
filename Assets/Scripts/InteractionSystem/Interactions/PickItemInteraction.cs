using UnityEngine;

public class PickItemInteraction : MonoBehaviour
{
    public GameObject objectToPick;

    public void PickItem()
    {
        ItemHolder.instance.PickItem(objectToPick);
    }
}
