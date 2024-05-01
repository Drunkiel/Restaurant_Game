using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WashItemInteraction : MonoBehaviour
{
    public GameObject hint;
    private Slider progressSlider;
    public PlaceItemInteraction _placeItem;

    private void Start()
    {
        progressSlider = hint.GetComponentInChildren<Slider>();
    }

    public void Check()
    {
        //Checks if there is more than one item
        if (_placeItem.holdingItems.Count <= 0)
            return;

        //Checks if item is dish || should never work
        if (_placeItem.holdingItems[0]._dishItem.Equals(null))
        {
            Debug.LogError("Something went wrong with the: " + transform.name);
            return;
        }

        //Checks if item is dirty
        if (!_placeItem.holdingItems[0]._dishItem.isDirty)
        {
            hint.SetActive(false);
            return;
        }
        else
            hint.SetActive(true);
    }

    public void WashTheDish()
    {
        InteractionSystem.isInteracting = true;

        progressSlider.maxValue = 8;
        progressSlider.value = 0;
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
                _placeItem.holdingItems[0]._dishItem.ChangeDirtyState(false);
                InteractionSystem.isInteracting = false;
                Check();
            }
        }
    }
}
