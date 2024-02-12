using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "NPC/Names data")]
public class NPCNamesData : ScriptableObject
{
    [Header("Lists of NPC names")]
    public List<string> names = new();

    public string RandomName()
    {
        return names[Random.Range(0, names.Count)];
    }
}
