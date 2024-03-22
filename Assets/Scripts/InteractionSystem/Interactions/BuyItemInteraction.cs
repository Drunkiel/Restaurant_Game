using TMPro;
using UnityEngine;

public class BuyItemInteraction : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private TMP_Text priceTagText;
    [SerializeField] private PickItemInteraction _itemInteraction;

    private void Awake()
    {
        priceTagText.text = price + "$";
    }

    public void BuyItem()
    {
        MoneyManager _moneyManager = ProgressMetricController.instance._moneyManager;

        if (_moneyManager.GetAmount() >= price)
        {
            _itemInteraction.PickItem();
            _moneyManager.RemoveMoney(price);
        }
        else
            print($"Not enough money: {_moneyManager.GetAmount() - price}");
    }
}
