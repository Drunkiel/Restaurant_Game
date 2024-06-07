using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    public static PlayerLookController instance;

    public Texture2D mainPlayerTexture;
    public Texture2D playerTexture;
    public ColorPickerController _colorPicker;

    private void Awake()
    {
        instance = this;
        LoadTexture();
    }

    public void UpdateTexture(List<Vector2Int> points, Color32 newColor)
    {
        //Making changes to texture
        foreach (Vector2Int point in points)
        {
            int startX = point.x * 4;
            int endX = startX + 4;

            int startY = -(point.y + 1) * 4;
            int endY = startY + 4;

            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    playerTexture.SetPixel(x, y, newColor);
                }
            }
        }

        //Applying changes
        playerTexture.Apply();
        SaveTexture();
    }

    public void SaveTexture()
    {
        byte[] bytes = playerTexture.EncodeToPNG();
        string path = SaveLoadSystem.savePath + "Custom_Player_Look.png";
        File.WriteAllBytes(path, bytes);
    }

    public void LoadTexture()
    {
        string path = SaveLoadSystem.savePath + "Custom_Player_Look.png";
        byte[] bytes = File.ReadAllBytes(path);
        ImageConversion.LoadImage(playerTexture, bytes);
    }
}
