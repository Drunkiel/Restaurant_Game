using UnityEngine;
using UnityEngine.UI;

public class ColorPickerController : MonoBehaviour
{
    public Color32 color;
    [SerializeField] private byte rColor;
    [SerializeField] private byte gColor;
    [SerializeField] private byte bColor;
    [SerializeField] private Image previewImage; 
    [SerializeField] private ColorVariable[] colorVariables;

    public void UpdatePreview()
    {
        rColor = colorVariables[0].GetValue();
        gColor = colorVariables[1].GetValue();
        bColor = colorVariables[2].GetValue();

        color = new Color32(rColor, gColor, bColor, 255);
        previewImage.color = color;
    }

/*    public Color32 GetRandomColor()
    {
        // Randomize colors
        Color32 randomColor = new(
            (byte)Random.Range(0, 256),
            (byte)Random.Range(0, 256),
            (byte)Random.Range(0, 256),
            255
        );

        return randomColor;
    }*/
}
