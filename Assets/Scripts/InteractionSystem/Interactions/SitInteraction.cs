using UnityEngine;

public class SitInteraction : MonoBehaviour
{
    public ItemID _objectsID;
    private ActionController _actionController;

    private void Start()
    {
        RestaurantManager.instance._allSits.Add(this);
    }

    public void Sit()
    {
        //Ending action to prevent cloning NPC
        _actionController = _objectsID.GetComponent<ActionController>();
        _actionController.EndAction();

        if (_objectsID == null)
            return;

        //Recreating NPC
        GameObject newItem = Instantiate(_objectsID.gameObject, transform);

        //Freezing rotation
        newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        //Setting NPC on new position
        newItem.transform.SetLocalPositionAndRotation(new Vector3(0, 0.02f, 0.1f), Quaternion.identity);

        //Setting old name 
        newItem.name = _objectsID.gameObject.name;

        //Setting animation
        newItem.GetComponent<Animator>().SetBool("isSitting", true);
        Destroy(_objectsID.gameObject);
        _objectsID = newItem.GetComponent<ItemID>();
    }

    public void GetUpFromSeat()
    {
        //Ending action to prevent cloning NPC
        _actionController = _objectsID.GetComponent<ActionController>();
        _actionController.EndAction();

        //Recreating NPC
        GameObject newItem = Instantiate(_objectsID.gameObject);

        //Freezing rotation
        Rigidbody rgBody = newItem.GetComponent<Rigidbody>();
        rgBody.constraints = RigidbodyConstraints.None;
        rgBody.constraints = RigidbodyConstraints.FreezeRotation;

        //Setting NPC on new position
        Transform seatParent = transform.parent.parent;
        Vector3 seatOffsetPosition = seatParent.GetChild(seatParent.childCount - 1).position;
        newItem.transform.position = seatOffsetPosition;

        //Setting old name 
        newItem.name = _objectsID.gameObject.name;
        Destroy(_objectsID.gameObject);
    }
}
