using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCard : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;

    public void SetBuildingData(BuildingID _buildingID)
    {
        SetCardData(_buildingID.showcaseImage, _buildingID.buildingName);
        button.onClick.AddListener(() => BuildingSystem.instance.InitializeWithObject(_buildingID.gameObject));
    }

    public void SetCardData(Sprite sprite, string text)
    {
        image.sprite = sprite;
        this.text.text = text;
    }
}
