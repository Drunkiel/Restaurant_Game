using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Building/Building data")]
public class BuildingObjectData : ScriptableObject
{
    [Header("Lists of objects to build divided into several sections")]
    public List<BuildingID> _furnituresIDList = new();
    public List<BuildingID> _wallsIDList = new();
    public List<BuildingID> _floorsIDList = new();
    public List<BuildingID> _decorationsIDList = new();
}
