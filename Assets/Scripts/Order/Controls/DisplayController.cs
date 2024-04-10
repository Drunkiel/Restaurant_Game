using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour
{
    [SerializeField] private Image displayImage;
    [SerializeField] private TMP_Text nameText;

    public void UpdateDisplay(Sprite sprite, string name)
    {
        displayImage.sprite = sprite;
        nameText.text = name;
    }
}
