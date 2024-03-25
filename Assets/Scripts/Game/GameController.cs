using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isGamePaused;
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
        _timeController.clockText.GetComponent<Animator>().SetBool("isBlinking", false);
        _timeController.ShowDay();
        OrderController.instance.DestroyOrders();
        SummaryController.instance.ResetSummary();
    }

    private void SpawnNPC()
    {
        _spawnController.SpawnNewNPC();
    }

    private void EndDay()
    {
        _doorsAnimation.ChangeAnimation();
        _timeController.clockText.GetComponent<Animator>().SetBool("isBlinking", true);
        SummaryController.instance.MakeSummary();
    }

    private bool CheckIfCanEndShift()
    {
        OrderController _orderController = OrderController.instance;

        //Check if all customers were served
        if (_timeController.GetTime().x >= 15 && !_orderController.finishedOrders.Equals(_orderController.countOfOrdersToEnd))
            return false;

        return true;
    }
}
