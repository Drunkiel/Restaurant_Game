using TMPro;
using UnityEngine;

[System.Serializable]
public class MoneyManager
{
    [SerializeField] private int money;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text[] popUps;

    public int GetAmount()
    {
        return money;
    }

    public void AddMoney(int amount, bool expense = true)
    {
        ManageMoney(amount, expense);
        SummaryController.instance.income += amount;
    }

    public void RemoveMoney(int amount, bool expense = true)
    {
        if (expense && money - amount < 0)
            return;

        ManageMoney(-amount, expense);
        if (expense)
            SummaryController.instance.expenses += amount;
    }

    private void ManageMoney(int amount, bool show)
    {
        money += amount;
        moneyText.text = money + "$";

        int a()
        {
            if (amount > 0) return 0;
            else return 1;
        }

        if (!show)
            return;

        popUps[a()].text = amount + "$";
        popUps[a()].GetComponent<Animator>().Play("MoneyText");
    }
}