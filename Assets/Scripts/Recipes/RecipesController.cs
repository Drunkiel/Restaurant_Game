using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
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

public class RecipesController : MonoBehaviour
{
    public static RecipesController instance;

    public List<Recipe> processedRecipes = new();
    public List<Recipe> finishedRecipes = new();

    private void Awake()
    {
        instance = this;
    }

    public int FindRecipe(MakingProcess process, List<ItemID> holdingItems)
    {
        switch (process)
        {
            case MakingProcess.Chopping:
                return LookForRecipe(processedRecipes, holdingItems);

            case MakingProcess.Cooking:
                return LookForRecipe(processedRecipes, holdingItems);

            case MakingProcess.Combining:
                return LookForRecipe(finishedRecipes, holdingItems);

            default:
                return -1;
        }
    }

    private int LookForRecipe(List<Recipe> recipes, List<ItemID> holding)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].requiredItems.Count == holding.Count - 1)
            {
                List<bool> isHavingItem = new();
                for (int j = 1; j < holding.Count; j++)
                {
                    isHavingItem.Add(recipes[i].requiredItems[j - 1].itemName.Equals(holding[j].itemName));
                }

                for (int j = 0; j < isHavingItem.Count; j++)
                {
                    if (!isHavingItem[j])
                    {
                        isHavingItem.Clear();
                        return -1;
                    }
                }

                isHavingItem.Clear();
                return i;
            }
        }

        return -1;
    }
}
