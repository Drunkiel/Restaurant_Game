using UnityEngine;
using UnityEngine.UI;

public class ColorPickerController : MonoBehaviour
{
    public Color32 color;
    [SerializeField] private byte rColor;
    [SerializeField] private byte gColor;
    [SerializeField] private byte bColor;
    [SerializeField] private Image previewImage;
    public Button applyBTN;
    public Button resetBTN;
    public Button resetAllBTN;
    [SerializeField] private ColorVariable[] colorVariables;

    public void UpdatePreview()
    {
        rColor = colorVariables[0].GetValue();
        gColor = colorVariables[1].GetValue();
        bColor = colorVariables[2].GetValue();

        color = new Color32(rColor, gColor, bColor, 255);
        previewImage.color = color;
    }

    public void UpdateVariables(Image image)
    {
        Color32 color = image.color;

        colorVariables[0].SetValue(color.r);
        colorVariables[1].SetValue(color.g);
        colorVariables[2].SetValue(color.b);

        UpdatePreview();
    }
}
