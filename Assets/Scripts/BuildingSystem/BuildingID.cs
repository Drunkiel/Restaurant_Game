using UnityEngine;

public enum BuildGrid
{
    FurnituresGrid,
    BuildingGrid
}

public class BuildingID : MonoBehaviour
{
    public string buildingName;
    public BuildGrid grid;
    public Sprite showcaseImage;
}
