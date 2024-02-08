using System.Collections.Generic;
using UnityEngine;

public class GoToAction : MonoBehaviour
{
    public List<Vector3> positions = new();

    private NPCController _NPCController;
    private ActionController _actionController;

    private void Start()
    {
        _NPCController = GetComponent<NPCController>();
        _actionController = GetComponent<ActionController>();
    }

    public void GoToPosition(int whichPosition)
    {
        if (Vector3.Distance(transform.position, positions[whichPosition]) < _actionController.thingsToAchieve[_actionController.actionIndex].distance)
            _actionController.thingsToAchieve[_actionController.actionIndex].isActionDone = true;

        if (!_actionController.thingsToAchieve[_actionController.actionIndex].isActionDone)
        {
            Vector3 direction = positions[whichPosition] - transform.position;
            _NPCController.movement = new Vector3(direction.x * _NPCController.speed, transform.position.y, direction.z * _NPCController.speed);
        }
        else
        {
            _NPCController.movement = Vector2.zero;
            _actionController.EndAction();
        }
    }

    public void GetSitPosition()
    {
        RestaurantManager _manager = RestaurantManager.instance;
        int sitIndex = _manager.LookForAvailableSit();
        if (sitIndex == -1) return;
        _manager.allSits[sitIndex].GetComponent<PlacableObject>()._interactableObject.GetComponent<SitInteraction>()._objectsID = GetComponent<ItemID>();

        Transform sit = _manager.allSits[sitIndex].transform;
        positions.Add(sit.position);
        _actionController.thingsToAchieve[_actionController.actionIndex].isActionDone = true;
        _actionController.EndAction();
    }
}
