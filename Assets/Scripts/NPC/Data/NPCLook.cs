using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "NPC/Look data")]
public class NPCLook : ScriptableObject
{
    [Header("Lists of NPC names")]
    [SerializeField] private List<Mesh> hairMeshes = new();
    [SerializeField] private Texture2D atlas;

    public Mesh RandomHair()
    {
        return hairMeshes[Random.Range(0, hairMeshes.Count)];
    }

    public Color32 GetRandomColor()
    {
        // Randomize colors
        Color32 randomColor = new(
            (byte)Random.Range(0, 256),
            (byte)Random.Range(0, 256),
            (byte)Random.Range(0, 256),
            255
        );

        return randomColor;
    }

    public void ChangeTexture(List<Vector2Int> points, Color32 color)
    {
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
                    atlas.SetPixel(x, y, color);
                }
            }
        }

        atlas.Apply();
    }
}