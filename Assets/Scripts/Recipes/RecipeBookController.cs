using System.Collections.Generic;
using UnityEngine;

public class RecipeBookController : MonoBehaviour
{
    public static RecipeBookController instance;

    public List<OrderCard> _orderCards;

    private void Awake()
    {
        instance = this;
    }

    public void PickRecipe(ItemID _itemID)
    {
        print(_itemID.itemName);
        RecipeData _recipeData = RecipesController.instance.FindRecipeByItem(_itemID);

        if (_recipeData == null)
            return;

        for (int i = 0; i < _orderCards.Count - 1; i++)
        {
            if (_recipeData.requiredItems.Count - 1 < i)
            {
                _orderCards[i].button.interactable = false;
                _orderCards[i].SetCardData(_orderCards[i].equippedImage.sprite, "Null");
            }
            else
            {
                ItemID _requiredItem = _recipeData.requiredItems[i];
                _orderCards[i].SetCardData(_requiredItem.itemSprite, _requiredItem.itemName);
                _orderCards[i].button.interactable = true;
                _orderCards[i].AssignButton(_itemID);
            }
        }
        //Add result item to be seen
    }
}
