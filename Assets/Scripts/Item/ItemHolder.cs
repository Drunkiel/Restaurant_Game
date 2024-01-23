using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder instance;

    public bool isHoldingItem;
    public bool isHoldingStackableItem;

    [SerializeField] private Transform holderTransform;
    [SerializeField] private Transform itemPlaceTransform;

    private void Awake()
    {
        instance = this;
    }

    public void PickItem(GameObject item)
    {
        if (!isHoldingItem)
        {
            GameObject newItem = Instantiate(item, itemPlaceTransform);
            newItem.transform.localScale /= 100;
            itemPlaceTransform.GetChild(0).GetChild(1).gameObject.SetActive(false);

            isHoldingItem = true;
            Destroy(item);
            return;
        }
        
        if (!isHoldingStackableItem)
        {
            GameObject newItem = Instantiate(item, holderTransform);
            newItem.transform.localScale /= 100;
            holderTransform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            isHoldingStackableItem = true;
            Destroy(item);
            return;
        }
    }
}
