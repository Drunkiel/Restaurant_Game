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

    public void UpdatePreviewImage()
    {
        previewImage.color = color;
    }

    private Color32 GetColor()
    {
        return PlayerLookController.instance.playerTexture.GetPixel(pixelTexturePosition.x * 4, -(pixelTexturePosition.y + 1) * 4);
    }
}
