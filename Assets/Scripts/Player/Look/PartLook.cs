using UnityEngine;
using UnityEngine.UI;

public class PartLook : MonoBehaviour
{
    public Vector2Int pixelTexturePosition;
    public Color32 color;
    [SerializeField] private Image previewImage;

    private void Start()
    {
        color = GetColorByTexture(PlayerLookController.instance.playerTexture);
        UpdatePreviewImage();

        PlayerLookController.instance._colorPicker.resetBTN.onClick.AddListener(() => ResetChanges());
    }

    public void SetApplyChanges()
    {
        PlayerLookController _lookController = PlayerLookController.instance;
        _lookController._colorPicker.applyBTN.onClick.RemoveAllListeners();
        _lookController._colorPicker.applyBTN.onClick.AddListener(() =>
        {
            _lookController.UpdateTexture(new() { new(pixelTexturePosition.x, pixelTexturePosition.y) }, _lookController._colorPicker.color);
            color = GetColorByTexture(PlayerLookController.instance.playerTexture);
            UpdatePreviewImage();
            _lookController.SaveTexture();
            _lookController._colorPicker.transform.GetChild(0).gameObject.SetActive(false);
        });

        _lookController._colorPicker.resetBTN.onClick.RemoveAllListeners();
        _lookController._colorPicker.resetBTN.onClick.AddListener(() =>
        {
            ResetChanges();
            _lookController._colorPicker.transform.GetChild(0).gameObject.SetActive(false);
        });
    }

    public void ResetChanges()
    {
        PlayerLookController _lookController = PlayerLookController.instance;
        color = GetColorByTexture(_lookController.mainPlayerTexture);
        UpdatePreviewImage();
        _lookController._colorPicker.UpdateVariables(previewImage);
        _lookController.UpdateTexture(new() { new(pixelTexturePosition.x, pixelTexturePosition.y) }, color);
        _lookController.SaveTexture();
    }

    public void UpdatePreviewImage()
    {
        previewImage.color = color;
    }

    private Color32 GetColorByTexture(Texture2D texture)
    {
        return texture.GetPixel(pixelTexturePosition.x * 4, -(pixelTexturePosition.y + 1) * 4);
    }
}
