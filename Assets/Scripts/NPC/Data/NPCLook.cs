using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "NPC/Look data")]
public class NPCLook : ScriptableObject
{
    [Header("Lists of NPC names")]
    [SerializeField] private List<Color32> eyesColors = new();
    [SerializeField] private List<Mesh> hairMeshes = new();
    public Material NPCMaterial;
    public Texture2D atlas;

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

    public Color32 AdjustBrightness(Color32 color)
    {
        // Calculating luminance L=0.299R + 0.587G + 0.114B
        float luminance = 0.299f * color.r + 0.587f * color.g + 0.114f * color.b;

        float brightnessAdjustmentFactor = 0.5f;

        if (luminance > 128)
        {
            // Making color darker
            return new Color32(
                (byte)(color.r * (1 - brightnessAdjustmentFactor)),
                (byte)(color.g * (1 - brightnessAdjustmentFactor)),
                (byte)(color.b * (1 - brightnessAdjustmentFactor)),
                255
            );
        }
        else
        {
            // Making color brighter
            return new Color32(
                (byte)Mathf.Clamp(color.r * (1 + brightnessAdjustmentFactor), 0, 255),
                (byte)Mathf.Clamp(color.g * (1 + brightnessAdjustmentFactor), 0, 255),
                (byte)Mathf.Clamp(color.b * (1 + brightnessAdjustmentFactor), 0, 255),
                255
            );
        }
    }

    public Color32 GetEyesColor()
    {
        return eyesColors[Random.Range(0, eyesColors.Count)];
    }
}