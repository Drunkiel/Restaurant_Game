using System.Collections.Generic;
using UnityEngine;

public class NPCLookController : MonoBehaviour
{
    public static NPCLookController instance;

    [SerializeField] private NPCLook _NPCLook;

    public List<Material> NPCMaterials = new();
    public List<Texture2D> NPCTextures = new();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < NPCMaterials.Count; i++)
        {
            //Hair and shirt colors
            Color32 newColor = _NPCLook.GetRandomColor();
            UpdateTexture(i, new() { new(6, 5) }, newColor); //X and Y [0 to 15]
            UpdateTexture(i, new() { new(5, 5) }, _NPCLook.AdjustBrightness(newColor)); //X and Y [0 to 15]

            //Eyes color
            UpdateTexture(i, new() { new(2, 5) }, _NPCLook.GetEyesColor()); //X and Y [0 to 15]
        }
    }

    private void UpdateTexture(int index, List<Vector2Int> points, Color32 color)
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
                    NPCTextures[index].SetPixel(x, y, color);
                }
            }
        }

        //Applying changes
        NPCTextures[index].Apply();
    }

    public Material GetMaterial()
    {
        int randomIndex = Random.Range(0, NPCMaterials.Count);
        return NPCMaterials[randomIndex];
    }

    public Mesh GetHairMesh()
    {
        return _NPCLook.RandomHair();
    }
}
