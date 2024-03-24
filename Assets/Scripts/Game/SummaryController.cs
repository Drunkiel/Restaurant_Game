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
        expencesText.text = $"-{expenses}$";
        incomeText.text = $"{income}$";
        moneySummaryText.text = $"{income - expenses}$";

        satisfiedCostomersText.text = $"{satisfiedCostomers}";
        satisfiedCostomersText.text = $"{unSatisfiedCostomers}";
    }
}
