using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookController : MonoBehaviour
{
    public static RecipeBookController instance;

    public List<OrderCard> _orderCards;
    public Image makeStuffImage;
    public TMP_Text makeText;
    [SerializeField] private Sprite[] makeSprites;
    [SerializeField] private GameObject bookUI;

    private void Awake()
    {
        instance = this;
    }

    public void PickRecipe(ItemID _itemID)
    {
        RecipeData _recipeData = RecipesController.instance.FindRecipeByItem(_itemID);

        if (_recipeData == null)
            return;

        bookUI.SetActive(true);

        //Setting engridients items data
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
                _orderCards[i].AssignButton(_requiredItem);
            }
        }

        //Setting making image data
        switch (_recipeData.process)
        {
            case MakingProcess.Chopping:
                makeStuffImage.sprite = makeSprites[0];
                makeText.text = "Chop";
                break;
            case MakingProcess.Cooking:
                makeStuffImage.sprite = makeSprites[1];
                makeText.text = "Cook";
                break;
            case MakingProcess.Combining:
                makeStuffImage.sprite = makeSprites[2];
                makeText.text = "Combine";
                break;
        }

        //Setting result item data
        ItemID _resultItem = _recipeData.resultItem;
        _orderCards[^1].SetCardData(_resultItem.itemSprite, _resultItem.itemName);
    }
}
