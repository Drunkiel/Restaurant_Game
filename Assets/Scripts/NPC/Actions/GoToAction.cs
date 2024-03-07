using System.Collections.Generic;
using UnityEngine;

public class GoToAction : MonoBehaviour
{
    public List<Vector3> positions = new();
    private int randomPositionIndex;

    private NPCController _NPCController;
    private ActionController _actionController;

    private void Start()
    {
        _NPCController = GetComponent<NPCController>();
        _actionController = GetComponent<ActionController>();
        randomPositionIndex = Random.Range(0, positions.Count);
    }

    public void GoToPositionIndex(int whichPosition)
    {
        if (whichPosition == -1)
            GoTo(positions[randomPositionIndex]);
        else
            GoTo(positions[whichPosition]);

    }

    public void GoToPosition(Vector3 position)
    {
        GoTo(position);
    }

    public void GoToDoors(int index)
    {
        RestaurantManager _manager = RestaurantManager.instance;

        switch (index)
        {
            case 0:
                GoTo(_manager._doorID.transform.position + new Vector3(0, 0, -1f));
                break;

            case 1:
                GoTo(_manager._doorID.transform.position + new Vector3(0, 0, 0.3f));
                break;
        }
    }

    public void GoToSeat()
    {
        GoTo(GetComponent<SitAction>()._seatID.transform.position);
    }

    private void GoTo(Vector3 position)
    {
        bool isNearby = false;

        if (Vector3.Distance(transform.position, position) < _actionController.thingsToAchieve[_actionController.actionIndex].distance)
            isNearby = true;

        if (!isNearby)
        {
            Vector3 direction = position - transform.position;
            _NPCController.movement = new Vector2(
                direction.x * _NPCController.speed,
                direction.z * _NPCController.speed
                );
        }
        else
        {
            _NPCController.movement = Vector2.zero;
            _actionController.EndAction();
        }
    }
}
