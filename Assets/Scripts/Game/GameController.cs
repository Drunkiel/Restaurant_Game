using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TimeController _timeController;
    [SerializeField] private NPCSpawnController _spawnController;

    public void StartNewDay()
    {
        _timeController.NewDay();
    }
}
