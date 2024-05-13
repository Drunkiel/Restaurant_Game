using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnController : MonoBehaviour
{
    public int spawnedNPCCounter;

    [SerializeField] private List<Vector3> spawnPoints = new();
    [SerializeField] private GameObject npcPrefab;
    private readonly List<Vector2> npcTimeSpawn = new() { new(45, 60), new(35, 50), new(25, 40), new(20, 30), new(10, 20), new(5, 15) };
    public readonly List<Vector2> npcFirstSpawn = new() { new(4, 8), new(3, 6), new(3, 5), new(2, 4), new(1, 3), new(0, 2) }; 

    public void SpawnNewNPC()
    {
        int actualHour = (int)TimeController.instance.GetTime().x;

        if (actualHour < 15)
        {
            Vector2 timeSpan = npcTimeSpawn[ProgressMetricController.instance._ratingManager.GetRating()];
            Invoke(nameof(SpawnNewNPC), Random.Range(timeSpan.x, timeSpan.y));
        }
        else
            return;

        OrderController.instance.countOfOrdersToEnd += 1;
        spawnedNPCCounter += 1;
        Instantiate(npcPrefab, GetSpawnPoint(), Quaternion.identity);
    }

    private Vector3 GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
