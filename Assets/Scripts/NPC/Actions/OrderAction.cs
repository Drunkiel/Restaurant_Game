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
        if (anim.GetBool("isEating"))
            return;

        OrderData _data = _currentOrder._card._data;
        Invoke(nameof(FinishMeal), Random.Range(_data.minTimeToFinish, _data.maxTimeToFinish));
        _itemInteraction.holdingItems[0].isPickable = false;
        _itemInteraction.holdingItems[0].transform.localPosition = new Vector3(0, -0.014f, 0.265f);
        anim.SetBool("isEating", true);
    }

    private void FinishMeal()
    {
        ProgressMetricController _progressController = ProgressMetricController.instance;

        //Making changes to dish and NPC
        DestroyFood();
        anim.SetBool("isEating", false);
        _itemInteraction.holdingItems[0].transform.localPosition = Vector3.zero;
        _itemInteraction.holdingItems[0].isPickable = true;
        _progressController._ordersManager.IncreaseFinishedOrdersCounter();

        //Setting statistics
        OrderController.instance.countOfOrdersToEnd -= 1;
        _progressController._moneyManager.AddMoney(_currentOrder._card._data.price);
        _progressController._ratingManager.currentRating += 
            _ratingAction.GiveRating(
                _patienceController.waitingTime,
                _patienceController.maxWaitingTime[_progressController._ratingManager.GetRating()]
                );

        //Making changes to objects
        _itemInteraction.holdingItems[0]._dishItem.ChangeDirtyState(true);
        Destroy(_currentOrder._card.gameObject);

        //Ending action
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

        //Assigning patience data
        _patienceController.waitingTime += Time.deltaTime;
        _patienceController.waitingSlider.value = Mathf.RoundToInt(_patienceController.waitingTime);

        if (_patienceController.waitingTime > _patienceController.maxWaitingTime[ProgressMetricController.instance._ratingManager.GetRating()])
            CancelOrder();

        //Check if order is served
        if (_itemID != null && !_dishItemID._dishItem.isDirty && _itemID.itemID.Equals(_currentOrder._card._data._itemID.itemID))
        {
            _patienceController.waitingSlider.gameObject.SetActive(false);
            _actionController.EndAction();
        }
    }

    public void CancelOrder()
    {
        ProgressMetricController _progressController = ProgressMetricController.instance;

        //Reseting state
        Destroy(_currentOrder._card.gameObject);
        _patienceController.waitingSlider.gameObject.SetActive(false);

        //Setting statistics
        OrderController.instance.countOfOrdersToEnd -= 1;
        SummaryController.instance.unSatisfiedCostomers += 1;
        _progressController._ratingManager.currentRating += 
            _ratingAction.GiveRating(
                _patienceController.waitingTime,
                _patienceController.maxWaitingTime[_progressController._ratingManager.GetRating()]
                );

        //Skiping actions
        _actionController.SkipAction();
    }
}
