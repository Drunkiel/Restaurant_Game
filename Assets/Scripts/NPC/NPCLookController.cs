using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCLookController : MonoBehaviour
{
    [SerializeField] private NPCLook _NPCLook;

    [SerializeField] private SkinnedMeshRenderer eyesRenderer;
    [SerializeField] private SkinnedMeshRenderer hairRenderer;
    [SerializeField] private SkinnedMeshRenderer bodyRenderer;

    public Material NPCMaterial;
    public Texture2D NPCTexture;

    public void ChangeTexture(List<Vector2Int> points, Color32 color)
    {
        //Copying material
        NPCMaterial = new(_NPCLook.NPCMaterial.shader);
        NPCMaterial.name = "test";
        NPCMaterial.CopyPropertiesFromMaterial(_NPCLook.NPCMaterial);

        //Copying texture
        NPCTexture = new(_NPCLook.atlas.width, _NPCLook.atlas.height);
        NPCTexture.SetPixels32(_NPCLook.atlas.GetPixels32());

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
                    NPCTexture.SetPixel(x, y, color);
                }
            }
        }

        //Applying changes
        NPCMaterial.SetTexture("_MainTex", NPCTexture);
        NPCTexture.Apply();
        SaveLoadSystem.SaveTextureToFile(NPCTexture, SaveLoadSystem.savePath + "test.png");
    }

    public void UpdateLook()
    {
        Mesh newHair = _NPCLook.RandomHair();

        //Hair and shirt colors
        List<Vector2Int> points = new() { new(6, 5), new(5, 5) };
        ChangeTexture(points, _NPCLook.GetRandomColor()); //X and Y [0 to 15]

        //Eyes color
        points = new() { new(5, 5) };
        ChangeTexture(points, _NPCLook.GetRandomColor()); //X and Y [0 to 15]

        if (newHair == null)
        {
            hairRenderer.gameObject.SetActive(false);
            return;
        }

        hairRenderer.sharedMesh = newHair;
        hairRenderer.materials[0] = NPCMaterial;
        eyesRenderer.materials[0] = NPCMaterial;
        bodyRenderer.materials[0] = NPCMaterial;
    }
}
