using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCard : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;

    public void SetData(BuildingID _buildingID)
    {
        image.sprite = _buildingID.showcaseImage;
        text.text = _buildingID.buildingName;
        button.onClick.AddListener(() => BuildingSystem.instance.InitializeWithObject(_buildingID.gameObject));
    }
}
