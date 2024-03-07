using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isGameStarted;

    [SerializeField] private AnimationInteraction _doorsAnimation;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private NPCSpawnController _spawnController;

    public void ManageGameplay()
    {
        if (!isGameStarted)
        {
            StartNewDay();
            isGameStarted = true;
        }
        else if (CheckIfCanEndShift())
        {
            EndDay();
            isGameStarted = false;
        }
    }

    private void StartNewDay()
    {
        _doorsAnimation.ChangeAnimation();
        _timeController.NewDay();
        Invoke(nameof(SpawnNPC), Random.Range(2, 8));
        _timeController.clockText.gameObject.SetActive(true);
        OrderController.instance.DestroyOrders();
    }

    private void SpawnNPC()
    {
        _spawnController.SpawnNewNPC();
    }

    private void EndDay()
    {
        _doorsAnimation.ChangeAnimation();
        _timeController.clockText.gameObject.SetActive(false);
    }

    private bool CheckIfCanEndShift()
    {
        //Check if all customers were served
        if (!OrderController.instance.finishedOrders.Equals(_spawnController.NPCsToSpawn))
            return false;

        return true;
    }
}
