using System.IO;
using UnityEngine;

public class GameController : SaveLoadSystem
{
    public static bool isGamePaused;
    public static bool isGameStarted;

    [SerializeField] private AnimationInteraction _doorsAnimation;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private NPCSpawnController _spawnController;

    private void Awake()
    {
        try
        {
            Load(savePath);
        }
        catch (System.Exception)
        {
            Save(savePath);
        }
    }

    public override void Save(string path)
    {
        path = savePath;

        //Here open file
        FileStream saveFile = new(path, FileMode.OpenOrCreate);

        //Here collect data to save
        ProgressMetricController _progress = ProgressMetricController.instance;

        _data.money = _progress._moneyManager.GetAmount();
        _data.currentRating = _progress._ratingManager.currentRating;
        _data.finishedOrders = _progress._ordersManager.finishedOrders;

        //Here save data to file
        string jsonData = JsonUtility.ToJson(_data, true);

        saveFile.Close();
        File.WriteAllText(path, jsonData);
    }

    public override void Load(string path)
    {
        path = savePath;

        //Here load data from file
        string saveFile = ReadFromFile(path);
        JsonUtility.FromJsonOverwrite(saveFile, _data);

        //Here override game data
        ProgressMetricController _progress = ProgressMetricController.instance;

        _progress._moneyManager.RemoveMoney(_progress._moneyManager.GetAmount(), false);
        _progress._moneyManager.AddMoney(_data.money);
        _progress._ratingManager.currentRating = _data.currentRating;
        _progress._ratingManager.UpdateRating();
        _progress._ordersManager.finishedOrders = _data.finishedOrders;
    }

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
        if (_timeController.GetTime().x >= 15 && _orderController.countOfOrdersToEnd == 0)
            return true;

        return false;
    }
}