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
                for (int i = 0; i < processedRecipes.Count; i++)
                {
                    if (processedRecipes[i].requiredItems.Count != holdingItems.Count - 1) break;

                    List<bool> isHavingItem = new();
                    for (int j = 1; j < holdingItems.Count; j++)
                    {
                        isHavingItem.Add(processedRecipes[i].requiredItems[j - 1].itemName.Equals(holdingItems[j].itemName));
                    }
                }
                break;

            case MakingProcess.Cooking:
                for (int i = 0; i < processedRecipes.Count; i++)
                {

                }
                break;

            case MakingProcess.Combining:
                for (int i = 0; i < finishedRecipes.Count; i++)
                {

                }
                break;
        }

        return -1;
    }
}
