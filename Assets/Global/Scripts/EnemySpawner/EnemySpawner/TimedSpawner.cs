using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TimedSpawner : MonoBehaviour
{
    public float spawnInterval = 1f; // Interval between spawns
    public float spawnDuration = 30f; // Total time to spawn enemies
    public List<EnemyPrefabCount> enemyChanceList; // List of enemies and their spawn chances
    public Transform spawnArea; // Area to spawn enemies

    private SingleEnemySpawnArea spawner = new SingleEnemySpawnArea();
    private float startTime;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        startTime = Time.time;
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        while (Time.time < spawnDuration + startTime)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        EndSpawning();
    }

    private void SpawnEnemy()
    {
        GameObject enemyprefab = RandomDistribution.GetRandom(EnemyPrefabCount.GetDict(enemyChanceList));
        
        GameObject spawnedEnemy = spawner.SpawnEnemy(enemyprefab, spawnArea, false);
        if (spawnedEnemy != null)
        {
            spawnedEnemies.Add(spawnedEnemy);
        }
    }

    private void EndSpawning()
    {
        // Destroy all spawned enemies
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }

        // Destroy the spawner itself
        Destroy(gameObject);
    }
}
