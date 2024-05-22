using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type
{
    None,
    Fridge,
    Shelves,
}

public class LoadItems : MonoBehaviour
{
    public Type itemType;
    [SerializeField] private List<ShopItemData> items = new();
    [SerializeField] private GameObject cardPrefab;

    private void Awake()
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, transform);
            BuyItemInteraction _itemInteraction = newCard.GetComponent<BuyItemInteraction>();
            _itemInteraction._itemData = items[i];

            BuildingID _buildingID = null;
            switch (itemType)
            {
                case Type.None:
                    return;

                case Type.Fridge:
                    _buildingID = RestaurantManager.instance._fridgeID;
                    break;

                case Type.Shelves:
                    _buildingID = RestaurantManager.instance._shelvesID;
                    break;
            }

            newCard.GetComponent<Button>().onClick.AddListener(() => _buildingID.transform.GetChild(1).GetComponent<OpenUIInteraction>().OpenUI());
        }
    }
}
