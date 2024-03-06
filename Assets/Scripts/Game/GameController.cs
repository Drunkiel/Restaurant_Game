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
        else
        {
            EndDay();
            isGameStarted = false;
        }
    }

    private void StartNewDay()
    {
        _doorsAnimation.ChangeAnimation();
        _timeController.NewDay();
        _timeController.clockText.gameObject.SetActive(true);
        OrderController.instance.DestroyOrders();
    }

    private void EndDay()
    {
        _doorsAnimation.ChangeAnimation();
        _timeController.clockText.gameObject.SetActive(false);
    }
}
