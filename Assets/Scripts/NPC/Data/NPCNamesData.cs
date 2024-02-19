using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "NPC/Names data")]
public class NPCNamesData : ScriptableObject
{
    [Header("Lists of NPC names")]
    public bool isNPCNamed;
    public string pickedName;
    public List<string> names = new();

    public string RandomName()
    {
        isNPCNamed = true;
        pickedName = names[Random.Range(0, names.Count)];
        return pickedName;
    }
}
