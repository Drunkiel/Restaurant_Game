using UnityEngine;
using UnityEngine.UI;

public class ChopItemInteraction : MonoBehaviour
{
    public MakingProcess makingProcess;
    public Slider progressSlider;
    public PlaceItemInteraction _placeItem;
    RecipeData recipeData;

    public void Chop()
    {
        if (recipeData.indexOfRecipe == -1) return;

        progressSlider.value += 1;

        if (progressSlider.value < progressSlider.maxValue) return;

        switch (recipeData.recipeList)
        {
            //processedFood
            case 0:
                ReplaceHoldingItem(RecipesController.instance.processedRecipes[recipeData.indexOfRecipe].resultItem);
                break;

            //FinishedFood
            case 1:
                ReplaceHoldingItem(RecipesController.instance.finishedRecipes[recipeData.indexOfRecipe].resultItem);
                break;
        }
        Check();
    }

    public void ReplaceHoldingItem(ItemID _itemID)
    {
        for (int i = 1; i < _placeItem.holdingItems.Count; i++)
        {
            Destroy(_placeItem.holdingItems[i].gameObject);
        }

        ItemID _neededItem = _placeItem.holdingItems[0];
        _placeItem.holdingItems.Clear();
        _placeItem.holdingItems.Add(_neededItem);
        _placeItem.holdingItems[0].stackedItems.Clear();
        ItemHolder.instance.Pick(_itemID, _placeItem.holdingItems[0].transform, _placeItem.holdingItems.Count, false);
        ItemID placedObject = _placeItem.holdingItems[0].transform.GetChild(_placeItem.holdingItems[0].transform.childCount - 1).GetComponent<ItemID>();
        placedObject.transform.localPosition = Vector3.zero;
        _placeItem.holdingItems.Add(placedObject);
        _placeItem.holdingItems[0].stackedItems.Add(placedObject);
    }

    public void Check()
    {
        recipeData = RecipesController.instance.FindRecipe(makingProcess, _placeItem.holdingItems);

        if (recipeData.indexOfRecipe == -1) progressSlider.gameObject.SetActive(false);
        else
        {
            progressSlider.value = progressSlider.minValue;
            progressSlider.gameObject.SetActive(true);
        }
    }
}
