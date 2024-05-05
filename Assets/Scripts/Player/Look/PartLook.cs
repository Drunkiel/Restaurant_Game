using UnityEngine;
using UnityEngine.UI;

public class PartLook : MonoBehaviour
{
    public Vector2Int pixelTexturePosition;
    public Color32 color;
    [SerializeField] private Image previewImage;

    private void Start()
    {
        color = GetColor();
        UpdatePreviewImage();
    }

    public void SetApplyChanges()
    {
        PlayerLookController _lookController = PlayerLookController.instance;
        _lookController._colorPicker.applyBTN.onClick.RemoveAllListeners();
        _lookController._colorPicker.applyBTN.onClick.AddListener(() =>
        {
            _lookController.UpdateTexture(new() { new(pixelTexturePosition.x, pixelTexturePosition.y) }, _lookController._colorPicker.color);
            color = GetColor();
            UpdatePreviewImage();
        });
    }

    public void UpdatePreviewImage()
    {
        previewImage.color = color;
    }

    private Color32 GetColor()
    {
        return PlayerLookController.instance.playerTexture.GetPixel(pixelTexturePosition.x * 4, -(pixelTexturePosition.y + 1) * 4);
    }
}
