using UnityEngine;

public class SitAction : MonoBehaviour
{
    private ActionController _actionController;

    public ItemID _seatID;
    public SitInteraction _sitInteraction;

    private void Start()
    {
        _actionController = GetComponent<ActionController>();
    }

    public void GetSeat()
    {
        RestaurantManager _manager = RestaurantManager.instance;

        //Seat reservation
        if (_seatID == null) 
        {
            _seatID = _manager.LookForAvailableSit();
            if (_seatID == null) 
                return;
        }

        _sitInteraction = _seatID.GetComponent<PlacableObject>()._interactableObject.GetComponent<SitInteraction>();
        _sitInteraction._objectsID = GetComponent<ItemID>();
        _actionController.EndAction();
    }

    public void UseSeat()
    {
        _sitInteraction.Sit();
    }

    public void GetUp()
    {
        _sitInteraction.GetUpFromSeat();
    }
}
