using System.Collections.Generic;
using UnityEngine;

public class RestaurantManager : MonoBehaviour
{
    public static RestaurantManager instance;

    public BuildingID _doorID;
    public BuildingID _fridgeID;
    public BuildingID _shelvesID;

    public List<SitInteraction> _allSits = new();
    public List<BuildingID> frontWalls = new();
    public List<BuildingID> backWalls = new();
    public List<BuildingID> leftWalls = new();
    public List<BuildingID> rightWalls = new();
    public Material[] buildingMaterials;

    private void Awake()
    {
        instance = this;
        ChangeWallsVisibility();
    }

    public ItemID LookForAvailableSit()
    {
        List<SitInteraction> availableSeats = new();

        foreach (var _sit in _allSits)
        {
            if (_sit._objectsID == null)
                availableSeats.Add(_sit);
        }

        if (availableSeats.Count > 0)
            return availableSeats[Random.Range(0, availableSeats.Count)].GetComponentInParent<ItemID>();
        else
            return null;
    }

    public void ChangeWallsVisibility()
    {
        switch (CameraController.instance.playerState)
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
