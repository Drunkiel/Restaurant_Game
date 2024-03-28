using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnController : MonoBehaviour
{
    public int spawnedNPCCounter;

    [SerializeField] private List<Vector3> spawnPoints = new();
    [SerializeField] private GameObject npcPrefab;
    private readonly List<Vector2> npcTimeSpawn = new() { new(15, 40), new(13, 40), new(10, 35), new(8, 30), new(8, 20), new(6, 18) };

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
