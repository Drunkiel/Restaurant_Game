using UnityEngine;

public class OrderAction : MonoBehaviour
{
    public SingleOrder _currentOrder;
    private ActionController _actionController;
    [HideInInspector] public PlaceItemInteraction _itemInteraction; //DONT MAKE PRIVATE | THROWS ERRORS FOR SOME REASON
    private Animator anim;
    public NPCPatience _patienceController;
    public RatingAction _ratingAction;

    private void Start()
    {
        _actionController = GetComponent<ActionController>();
        anim = GetComponent<Animator>();
    }

    public void MakeOrder()
    {
        _patienceController.waitingSlider.maxValue = _patienceController.maxWaitingTime[ProgressMetricController.instance._ratingManager.GetRating()];
        _patienceController.waitingSlider.gameObject.SetActive(true);

        OrderController.instance.NewOrder(GetComponent<ItemID>(), this);
        _itemInteraction = GetComponent<SitAction>()._seatID.transform.parent.GetChild(0).GetComponent<PlaceItemInteraction>();
        _actionController.EndAction();
    }

    public void EatMeal()
    {
        if (!anim.GetBool("isEating"))
        {
            Invoke(nameof(FinishMeal), 5f);
            _itemInteraction.holdingItems[0].isPickable = false;
            _itemInteraction.holdingItems[0].transform.localPosition = new Vector3(0, -0.014f, 0.265f);
            anim.SetBool("isEating", true);
        }
    }

    private void FinishMeal()
    {
        ProgressMetricController _progressController = ProgressMetricController.instance;
        DestroyFood();

        anim.SetBool("isEating", false);
        _progressController._moneyManager.AddMoney(_currentOrder._card._data.price);
        _itemInteraction.holdingItems[0].transform.localPosition = Vector3.zero;
        _itemInteraction.holdingItems[0].isPickable = true;
        _progressController._ordersManager.IncreaseFinishedOrdersCounter();

        _progressController._ratingManager.currentRating += 
            _ratingAction.GiveRating(
                _patienceController.waitingTime,
                _patienceController.maxWaitingTime[ProgressMetricController.instance._ratingManager.GetRating()]
                );

        _itemInteraction.holdingItems[0]._dishItem.ChangeDirtyState(true);
        Destroy(_currentOrder._card.gameObject);
        _actionController.EndAction();
    }

    private void DestroyFood()
    {
        ItemID _itemID = _itemInteraction.GetHoldingItem();
        _itemInteraction.holdingItems.RemoveAt(_itemInteraction.holdingItems.Count - 1);
        _itemInteraction.holdingItems[0].GetComponent<DishItem>().stackedItems.Clear();
        Destroy(_itemID.gameObject);
    }

    public void CheckForOrder()
    {
        ItemID _itemID = _itemInteraction.GetHoldingItem();
        ItemID _dishItemID = _itemInteraction.GetDishItem();
        _patienceController.waitingTime += Time.deltaTime;
        _patienceController.waitingSlider.value = Mathf.RoundToInt(_patienceController.waitingTime);

        if (_patienceController.waitingTime > _patienceController.maxWaitingTime[ProgressMetricController.instance._ratingManager.GetRating()])
            CancelOrder();

        if (_itemID != null && !_dishItemID._dishItem.isDirty && _itemID.itemID.Equals(_currentOrder._card._data._itemID.itemID))
        {
            _patienceController.waitingSlider.gameObject.SetActive(false);
            _actionController.EndAction();
        }
    }

    public void CancelOrder()
    {
        OrderController.instance.countOfOrdersToEnd -= 1;
        SummaryController.instance.unSatisfiedCostomers += 1;
        Destroy(_currentOrder._card.gameObject);
        _patienceController.waitingSlider.gameObject.SetActive(false);

        ProgressMetricController.instance._ratingManager.currentRating += 
            _ratingAction.GiveRating(
                _patienceController.waitingTime,
                _patienceController.maxWaitingTime[ProgressMetricController.instance._ratingManager.GetRating()]
                );
        _actionController.SkipAction();
    }
}
