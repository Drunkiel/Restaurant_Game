using System.Collections.Generic;
using UnityEngine;

public enum MakingProcess
{
    Chopping,
    Cooking,
    Combining
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

    public Recipe FindRecipe(MakingProcess process, List<ItemID> _holdingItems)
    {
        return process switch
        {
            MakingProcess.Chopping => LookForRecipe(_recipes.choppingRecipes, 0, _holdingItems),
            MakingProcess.Cooking => LookForRecipe(_recipes.cookingRecipes, 1, _holdingItems),
            MakingProcess.Combining => LookForRecipe(_recipes.combiningRecipes, 2, _holdingItems),
            _ => new Recipe() { recipeList = 0, indexOfRecipe = -1 },
        };
    }

    private Recipe LookForRecipe(List<RecipeData> recipes, int listIndex, List<ItemID> _holdingItems)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].requiredItems.Count == _holdingItems.Count - 1)
            {
                for (int j = 0; j < recipes[i].requiredItems.Count; j++)
                {
                    if (recipes[i].requiredItems[j].itemID.Equals(_holdingItems[j + 1].itemID) &&
                        _holdingItems[0]._dishItem.process.Equals(recipes[i].process))
                    {
                        return new Recipe() { recipeList = listIndex, indexOfRecipe = i };
                    }
                }
            }
        }

        return new Recipe() { recipeList = listIndex, indexOfRecipe = -1 };
    }
}
