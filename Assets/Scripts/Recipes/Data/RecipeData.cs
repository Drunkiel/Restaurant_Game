using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Recipe/Recipe data")]
public class RecipeData : ScriptableObject
{
    public MakingProcess process;
    public List<ItemID> requiredItems = new();
    public ItemID resultItem;
    public int timeToMake; //In seconds
    public bool needInteraction;
}