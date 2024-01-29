using UnityEngine;

public class ChopItemInteraction : MonoBehaviour
{
    public MakingProcess makingProcess;
    public PlaceItemInteraction _placeItem;

    public void Chop()
    {
        RecipesController.instance.FindRecipe(makingProcess, _placeItem.holdingItems);
    }
}
