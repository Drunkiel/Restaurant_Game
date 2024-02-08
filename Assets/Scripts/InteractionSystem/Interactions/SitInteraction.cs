using UnityEngine;

public class SitInteraction : MonoBehaviour
{
    public ItemID _objectsID;

    [SerializeField] private ActionController _actionController;

    public void Sit()
    {
        _actionController = _objectsID.GetComponent<ActionController>();
        _actionController.thingsToAchieve[_actionController.actionIndex].isActionDone = true;
        _actionController.EndAction();

        GameObject newItem = Instantiate(_objectsID.gameObject, transform);
        newItem.transform.SetLocalPositionAndRotation(new Vector3(0, 0.002f, 0.1f), Quaternion.identity);
        newItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        newItem.name = _objectsID.gameObject.name;
        newItem.GetComponent<Animator>().SetBool("isSitting", true);
        Destroy(_objectsID.gameObject);
    }
}
