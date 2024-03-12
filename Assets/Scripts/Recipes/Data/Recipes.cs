using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Recipe/Recipes")]
public class Recipes : ScriptableObject
{
    public List<RecipeData> choppingRecipes = new();
    public List<RecipeData> cookingRecipes = new();
    public List<RecipeData> combiningRecipes = new();
}
