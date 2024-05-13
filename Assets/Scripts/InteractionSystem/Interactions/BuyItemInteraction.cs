using TMPro;
using UnityEngine;

public class BuyItemInteraction : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceTagText;
    [SerializeField] private PickItemInteraction _itemInteraction;

    private void Awake()
    {
        nameText.text = _itemInteraction._objectsID.itemName;
        priceTagText.text = price + "$";
    }

    public void BuyItem()
    {
        MoneyManager _moneyManager = ProgressMetricController.instance._moneyManager;

        //Checks if player can buy more
        if (!_itemInteraction.CanPickItem())
            return;

        //Checks if player has enough money
        if (_moneyManager.GetAmount() >= price)
        {
            _itemInteraction.PickItem();
            _moneyManager.RemoveMoney(price);
        }
        else
            print($"Not enough money: {_moneyManager.GetAmount() - price}");
    }
}
