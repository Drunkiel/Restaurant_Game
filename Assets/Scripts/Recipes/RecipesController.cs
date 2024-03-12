using System.Collections.Generic;
using UnityEngine;

public enum MakingProcess
{
    Chopping,
    Cooking,
    Combining,
}

[System.Serializable]
public class Recipe
{
    [Range(0, 1)]
    public int recipeList; //Between 0 and 1
    public int indexOfRecipe;
}

public class RecipesController : MonoBehaviour
{
    public static RecipesController instance;

    public Recipes _recipes;

    private void Awake()
    {
        instance = this;
    }

    public Recipe FindRecipe(MakingProcess process, List<ItemID> holdingItems)
    {
        return process switch
        {
            MakingProcess.Chopping => LookForRecipe(_recipes.choppingRecipes, 0, holdingItems),
            MakingProcess.Cooking => LookForRecipe(_recipes.cookingRecipes, 1, holdingItems),
            MakingProcess.Combining => LookForRecipe(_recipes.combiningRecipes, 2, holdingItems),
            _ => new Recipe() { recipeList = 0, indexOfRecipe = -1 },
        };
    }

    private Recipe LookForRecipe(List<RecipeData> recipes, int listIndex, List<ItemID> holding)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].requiredItems.Count == holding.Count - 1)
            {
                for (int j = 0; j < recipes[i].requiredItems.Count; j++)
                {
                    if (recipes[i].requiredItems[j].itemID.Equals(holding[j + 1].itemID))
                        return new Recipe() { recipeList = listIndex, indexOfRecipe = i };
                }
            }
        }

        return new Recipe() { recipeList = listIndex, indexOfRecipe = -1 };
    }
}
