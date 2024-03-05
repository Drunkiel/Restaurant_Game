using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnController : MonoBehaviour
{
    public int NPCsToSpawn = 5;
    public int spawnedNPCCounter;

    [SerializeField] private List<Vector3> spawnPoints = new();
    [SerializeField] private GameObject npcPrefab;

    private void Start()
    {
        SpawnNewNPC();
    }

    public void SpawnNewNPC()
    {
        if (NPCsToSpawn <= 0)
            return;

        spawnedNPCCounter++;
        Instantiate(npcPrefab, GetSpawnPoint(), Quaternion.identity);
    }

    private Vector3 GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
