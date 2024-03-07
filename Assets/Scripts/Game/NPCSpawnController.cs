using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnController : MonoBehaviour
{
    public int NPCsToSpawn = 1;
    public int spawnedNPCCounter;

    [SerializeField] private List<Vector3> spawnPoints = new();
    [SerializeField] private GameObject npcPrefab;

    public void SpawnNewNPC()
    {
        int actualHour = (int)TimeController.instance.GetTime().x;

        if (NPCsToSpawn > spawnedNPCCounter && actualHour < 15)
            Invoke(nameof(SpawnNewNPC), Random.Range(8, 30));
        else if (actualHour >= 15)
        {
            NPCsToSpawn = spawnedNPCCounter;
            return;
        }

        spawnedNPCCounter++;
        Instantiate(npcPrefab, GetSpawnPoint(), Quaternion.identity);
    }

    private Vector3 GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
