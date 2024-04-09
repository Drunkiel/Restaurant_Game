using UnityEngine;

public class OrderControlPanel : MonoBehaviour
{
    public PriceController _priceController;
    public GameObject UI;

    public void ManageData(OrderData _data)
    {
        UI.SetActive(true);
        _priceController._data = _data;
        _priceController.SetData();
    }
}
