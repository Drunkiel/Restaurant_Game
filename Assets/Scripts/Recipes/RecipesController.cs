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
    public Recipes _allRecipes;

    private void Awake()
    {
        instance = this;
    }

    public RecipeData FindRecipeByItem(ItemID _itemID)
    {
        List<RecipeData> _recipeDatas = new();
        _recipeDatas.AddRange(_allRecipes.choppingRecipes);
        _recipeDatas.AddRange(_allRecipes.cookingRecipes);
        _recipeDatas.AddRange(_allRecipes.combiningRecipes);

        for (int i = 0; i < _recipeDatas.Count; i++)
        {
            if (_itemID.itemID.Equals(_recipeDatas[i].resultItem.itemID))
                return _recipeDatas[i];
        }

        return null;
    }

    public Recipe FindRecipe(MakingProcess process, List<ItemID> _holdingItems)
    {
        return process switch
        {
            MakingProcess.Chopping => LookForRecipe(_allRecipes.choppingRecipes, 0, _holdingItems),
            MakingProcess.Cooking => LookForRecipe(_allRecipes.cookingRecipes, 1, _holdingItems),
            MakingProcess.Combining => LookForRecipe(_allRecipes.combiningRecipes, 2, _holdingItems),
            _ => new Recipe() { recipeList = 0, indexOfRecipe = -1 },
        };
    }

    private Recipe LookForRecipe(List<RecipeData> _recipesList, int listIndex, List<ItemID> _holdingItems)
    {
        for (int i = 0; i < _recipesList.Count; i++)
        {
            if (_recipesList[i].requiredItems.Count == _holdingItems.Count - 1)
            {
                for (int j = 0; j < _recipesList[i].requiredItems.Count; j++)
                {
                    if (_recipesList[i].requiredItems[j].itemID.Equals(_holdingItems[j + 1].itemID) &&
                        _holdingItems[0]._dishItem.process.Equals(_recipesList[i].process))
                    {
                        return new Recipe() { recipeList = listIndex, indexOfRecipe = i };
                    }
                }
            }
        }

        return new Recipe() { recipeList = listIndex, indexOfRecipe = -1 };
    }
}
