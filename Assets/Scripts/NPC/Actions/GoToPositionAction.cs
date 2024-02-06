using System.Collections.Generic;
using UnityEngine;

public class GoToPositionAction : MonoBehaviour
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
        if (Vector3.Distance(transform.position, positions[whichPosition]) < 0.1f) _actionController.thingsToAchieve[whichPosition].isActionDone = true;

        if (!_actionController.thingsToAchieve[whichPosition].isActionDone)
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
}
