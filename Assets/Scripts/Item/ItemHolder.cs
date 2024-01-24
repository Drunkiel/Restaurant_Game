using UnityEngine;
using static UnityEditor.Progress;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder instance;

    public bool isHoldingItem;
    public bool isHoldingStackableItem;

    [SerializeField] private ItemID holdingItem;
    [SerializeField] private ItemID holdingStackableItem;

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

    public void PickItem(GameObject item)
    {
        if (!isHoldingItem)
        {
            GameObject newItem = Instantiate(item, itemPlaceTransform);
            newItem.transform.localScale /= 100;
            newItem.transform.localPosition = Vector3.zero;
            newItem.transform.localRotation = Quaternion.Euler(90f, 0, 0);
            newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            newItem.name = item.name;
            holdingItem = newItem.GetComponent<ItemID>();
            itemPlaceTransform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            isHoldingItem = true;
            Destroy(item);
            return;
        }

        if (!isHoldingStackableItem)
        {
            GameObject newItem = Instantiate(item, holderTransform);
            newItem.transform.localScale /= 100;
            newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            newItem.name = item.name;
            holderTransform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            isHoldingStackableItem = true;
            Destroy(item);
            return;
        }
    }

    public void DropItem()
    {
        if (isHoldingItem)
        {
            itemPlaceTransform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            Vector3 spawnPosition = transform.position + transform.forward * 1/4;
            GameObject newItem = Instantiate(holdingItem.gameObject, spawnPosition, holdingItem.transform.rotation);
            newItem.transform.localScale *= 100;
            newItem.name = holdingItem.name;

            //Unfreezing position
            newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            Destroy(holdingItem.gameObject);
            isHoldingItem = false;
            return;
        }

        if (isHoldingStackableItem)
        {

        }
    }
}
