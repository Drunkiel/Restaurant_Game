using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderCard : MonoBehaviour
{
    public OrderData _data;

    [SerializeField] private Image image;
    public Image equippedImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button button;

    public void SetCardData(Sprite sprite, string text)
    {
        image.sprite = sprite;
        nameText.text = text;
    }

    public void UpdateEquipImage(bool isActive)
    {
        equippedImage.gameObject.SetActive(isActive);
    }

    public void Manage()
    {
        transform.parent.parent.parent.GetComponent<OrderControlPanel>().ManageData(this);
    }
}
