using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "NPC/Look data")]
public class NPCLook : ScriptableObject
{
    [Header("Lists of NPC names")]
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
}