using TMPro;
using UnityEngine;

public class SummaryController : MonoBehaviour
{
    public static SummaryController instance;
    public OpenCloseUI _summaryUI;

    public int expenses;
    public int income;
    public int satisfiedCostomers;
    public int unSatisfiedCostomers;

    [SerializeField] private TMP_Text expencesText;
    [SerializeField] private TMP_Text incomeText;
    [SerializeField] private TMP_Text moneySummaryText;

    [SerializeField] private TMP_Text satisfiedCostomersText;
    [SerializeField] private TMP_Text unSatisfiedCostomersText;
    [SerializeField] private TMP_Text ratingSummaryText;

    private void Awake()
    {
        instance = this;
    }

    public void MakeSummary()
    {
        _summaryUI.OpenClose();
        _summaryUI.UI.GetComponent<Animator>().SetTrigger("Show");

        expencesText.text = $"{expenses}$";
        incomeText.text = $"{income}$";
        moneySummaryText.text = $"{income - expenses}$";

        satisfiedCostomersText.text = $"{satisfiedCostomers}";
        unSatisfiedCostomersText.text = $"{unSatisfiedCostomers}";
    }

    public void ResetSummary()
    {
        if (_summaryUI.UI.activeSelf)
            _summaryUI.UI.GetComponent<Animator>().SetTrigger("Hide");

        expenses = 0;
        income = 0;
        satisfiedCostomers = 0;
        unSatisfiedCostomers = 0;
    }
}
