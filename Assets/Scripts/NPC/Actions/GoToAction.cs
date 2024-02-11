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

    public void GoToPositionIndex(int whichPosition)
    {
        GoTo(positions[whichPosition]);
    }

    public void GoToPosition(Vector3 position)
    {
        GoTo(position);
    }

    private void GoTo(Vector3 position)
    {
        bool isNearby = false;

        if (Vector3.Distance(transform.position, position) < _actionController.thingsToAchieve[_actionController.actionIndex].distance)
            isNearby = true;

        if (!isNearby)
        {
            Vector3 direction = position - transform.position;
            _NPCController.movement = new Vector3(direction.x * _NPCController.speed, transform.position.y, direction.z * _NPCController.speed);
        }
        else
        {
            _NPCController.movement = Vector2.zero;
            _actionController.EndAction();
        }
    }
}
