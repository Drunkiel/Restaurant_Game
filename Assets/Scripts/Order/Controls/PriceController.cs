using TMPro;
using UnityEngine;

public class PriceController : MonoBehaviour
{
    public int currentPrice;
    public int minimalPrice;
    public int maxPrice;

    public OrderData _data;
    [SerializeField] private TMP_Text priceText;

    public void SetData()
    {
        currentPrice = _data.price;
        minimalPrice = _data.minimalPrice;
        maxPrice = _data.maxPrice;
        UpdatePrice(0);
    }

    public void UpdatePrice(int i)
    {
        currentPrice += i;

        if (currentPrice < minimalPrice)
        {
            currentPrice = minimalPrice;
            return;
        }

        if (currentPrice > maxPrice)
        {
            currentPrice = maxPrice;
            return;
        }

        priceText.text = currentPrice + "$";
        _data.price = currentPrice;
    }
}
