using System.Collections.Generic;
using UnityEngine;

public class RestaurantManager : MonoBehaviour
{
    public static RestaurantManager instance;

    public List<ItemID> allSits = new();
    public List<BuildingID> frontWalls = new();
    public List<BuildingID> backWalls = new();
    public List<BuildingID> leftWalls = new();
    public List<BuildingID> rightWalls = new();
    public Material[] buildingMaterials;

    private void Awake()
    {
        instance = this;
        ChangeWallsVisibility(Camera.main.transform.position);
    }

    public int LookForAvailableSit()
    {
        List<ItemID> avalaibleSeats = new();

        for (int i = 0; i < allSits.Count; i++)
        {
            if (allSits[i].GetComponent<PlacableObject>()._interactableObject.GetComponent<SitInteraction>()._objectsID == null)
                avalaibleSeats.Add(allSits[i]);
        }

        if (avalaibleSeats.Count != 0)
            return Random.Range(0, avalaibleSeats.Count);

        return -1;
    }

    public void ChangeWallsVisibility(Vector3 position)
    {
        switch (CameraController.instance.state)
        {
            case 0:
                SetWallMaterial(frontWalls, 1);
                SetWallMaterial(backWalls, 0);
                SetWallMaterial(leftWalls, 0);
                SetWallMaterial(rightWalls, 0);
                break;

            case 1:
                SetWallMaterial(frontWalls, 0);
                SetWallMaterial(backWalls, 0);
                SetWallMaterial(leftWalls, 1);
                SetWallMaterial(rightWalls, 0);
                break;

            case 2:
                SetWallMaterial(frontWalls, 0);
                SetWallMaterial(backWalls, 1);
                SetWallMaterial(leftWalls, 0);
                SetWallMaterial(rightWalls, 0);
                break;

            case 3:
                SetWallMaterial(frontWalls, 0);
                SetWallMaterial(backWalls, 0);
                SetWallMaterial(leftWalls, 0);
                SetWallMaterial(rightWalls, 1);
                break;
        }
    }

    private void SetWallMaterial(List<BuildingID> walls, int index)
    {
        for (int i = 0; i < walls.Count; i++)
        {
            Transform wallTransform = walls[i].transform;
            MeshRenderer meshRenderer = wallTransform.GetChild(0).GetComponent<MeshRenderer>();

            meshRenderer.material = buildingMaterials[index];
        }
    }
}
