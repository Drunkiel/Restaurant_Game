using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MakingItemInteraction : MonoBehaviour
{
    public MakingProcess makingProcess;
    public GameObject hint;
    private Slider progressSlider;
    public PlaceItemInteraction _placeItem;
    private RecipeData _recipeData;
    private Recipe _recipe;

    private void Start()
    {
        progressSlider = hint.GetComponentInChildren<Slider>();
    }

    public void Make()
    {
        if (_recipeData == null || _recipeData.indexOfRecipe == -1) 
            return;

        MakeManually(_recipe.needInteraction);
    }

    private void MakeManually(bool startInteraction)
    {
        if (startInteraction) InteractionSystem.isInteracting = true;

        progressSlider.maxValue = _recipe.timeToMake;
        StartCoroutine(nameof(Wait));
    }

    IEnumerator Wait()
    {
        for (int i = 0; i < progressSlider.maxValue; i++)
        {
            yield return new WaitForSeconds(1f);
            progressSlider.value += 1;
            if (progressSlider.value >= progressSlider.maxValue)
            {
                FinishProcess();
                Check();
            }
        }
    }

    private void FinishProcess()
    {
        if (InteractionSystem.isInteracting) InteractionSystem.isInteracting = false;

        switch (_recipeData.recipeList)
        {
            //ProcessedFood
            case 0:
                ReplaceHoldingItem(_recipe.resultItem);
                break;

            //FinishedFood
            case 1:
                ReplaceHoldingItem(_recipe.resultItem);
                break;
        }
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
        placedObject.transform.localPosition = new Vector3(0, _itemID.heightPlacement, 0);
        _placeItem.holdingItems.Add(placedObject);
        _placeItem.holdingItems[0].stackedItems.Add(placedObject);
    }

    public void Check()
    {
        RecipesController _recipesController = RecipesController.instance;
        _recipeData = _recipesController.FindRecipe(makingProcess, _placeItem.holdingItems);

        if (_recipeData.indexOfRecipe == -1)
        {
            hint.SetActive(false);
            _recipe = new();
        }
        else
        {
            progressSlider.value = progressSlider.minValue;
            hint.SetActive(true);

            switch (_recipeData.recipeList)
            {
                //ProcessedFood
                case 0:
                    _recipe = _recipesController.processedRecipes[_recipeData.indexOfRecipe];
                    break;

                //FinishedFood
                case 1:
                    _recipe = _recipesController.finishedRecipes[_recipeData.indexOfRecipe];
                    break;
            }
        }
    }
}
