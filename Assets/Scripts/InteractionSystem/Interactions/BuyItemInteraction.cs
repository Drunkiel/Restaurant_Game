using TMPro;
using UnityEngine;

public class BuyItemInteraction : MonoBehaviour
{
    public ShopItemData _itemData;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceTagText;
    [SerializeField] private PickItemInteraction _itemInteraction;

    private void Start()
    {
        _itemInteraction._objectsID = _itemData._itemID;
        nameText.text = _itemInteraction._objectsID.itemName;
        priceTagText.text = _itemData.price + "$";
    }

    public void BuyItem()
    {
        MoneyManager _moneyManager = ProgressMetricController.instance._moneyManager;

        //Checks if player can buy more
        if (!_itemInteraction.CanPickItem())
            return;

        //Checks if player has enough money
        if (_moneyManager.GetAmount() >= _itemData.price)
        {
            _itemInteraction.PickItem();
            _moneyManager.RemoveMoney(_itemData.price);
        }
        else
            print($"Not enough money: {_moneyManager.GetAmount() - _itemData.price}");
    }
}
