using UnityEngine;

public class SitAction : MonoBehaviour
{
    private ActionController _actionController;

    public ItemID _seatID;
    private int sitIndex;

    private void Start()
    {
        _actionController = GetComponent<ActionController>();
    }

    public void GetSeat()
    {
        RestaurantManager _manager = RestaurantManager.instance;
        if (_seatID == null) 
        {
            sitIndex = _manager.LookForAvailableSit();
            if (sitIndex == -1) return;
        }

        //Seat reservation
        _manager.allSits[sitIndex].GetComponent<PlacableObject>()._interactableObject.GetComponent<SitInteraction>()._objectsID = GetComponent<ItemID>();
        _seatID = _manager.allSits[sitIndex];
        _actionController.GetComponent<GoToAction>().GoToPosition(_seatID.transform.position - new Vector3(0, 0, 0.5f));
    }

    public void UseSeat()
    {
        _seatID.GetComponent<PlacableObject>()._interactableObject.GetComponent<SitInteraction>().Sit();
    }

    public void GetUp()
    {
        _seatID.GetComponent<PlacableObject>()._interactableObject.GetComponent<SitInteraction>().GetUpFromSeat();
    }
}
