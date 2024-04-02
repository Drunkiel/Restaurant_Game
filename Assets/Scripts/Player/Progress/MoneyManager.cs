using TMPro;
using UnityEngine;

[System.Serializable]
public class MoneyManager 
{
    [SerializeField] private int money;
    [SerializeField] private TMP_Text moneyText;

    public void AddMoney(int amount)
    {
        money += amount;
        SummaryController.instance.income += amount;
        moneyText.text = money + "$";
    }

    public int GetAmount()
    {
        return money;
    }

    public void RemoveMoney(int amount, bool expense = true)
    {
        if (expense && money - amount < 0)
            return;

        money -= amount;
        if (expense)
            SummaryController.instance.expenses += amount;
        moneyText.text = money + "$";
    }
}
