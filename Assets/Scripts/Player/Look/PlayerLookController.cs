using System.Collections.Generic;
using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    public static PlayerLookController instance;

    public Texture2D playerTexture;
    public ColorPickerController _colorPicker;

    private void Awake()
    {
        instance = this;
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
    }
}
