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
    public MakingProcess process;
    public List<ItemID> requiredItems = new();
    public ItemID resultItem;
}

[System.Serializable]
public class RecipeData
{
    [Range(0, 1)]
    public int recipeList; //Between 0 and 1
    public int indexOfRecipe;
}

public class RecipesController : MonoBehaviour
{
    public static RecipesController instance;

    public List<Recipe> processedRecipes = new();
    public List<Recipe> finishedRecipes = new();

    private void Awake()
    {
        instance = this;
    }

    public RecipeData FindRecipe(MakingProcess process, List<ItemID> holdingItems)
    {
        return process switch
        {
            MakingProcess.Chopping => LookForRecipe(processedRecipes, 0, holdingItems),
            MakingProcess.Cooking => LookForRecipe(processedRecipes, 0, holdingItems),
            MakingProcess.Combining => LookForRecipe(finishedRecipes, 1, holdingItems),
            _ => new RecipeData() { recipeList = 0, indexOfRecipe = -1 },
        };
    }

    private RecipeData LookForRecipe(List<Recipe> recipes, int listIndex, List<ItemID> holding)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].requiredItems.Count == holding.Count - 1)
            {
                for (int j = 0; j < recipes[i].requiredItems.Count; j++)
                {
                    if (recipes[i].requiredItems[j].itemName.Contains(holding[j + 1].itemName))
                        return new RecipeData() { recipeList = listIndex, indexOfRecipe = i };
                }
            }
        }

        return new RecipeData() { recipeList = listIndex, indexOfRecipe = -1 };
    }
}
