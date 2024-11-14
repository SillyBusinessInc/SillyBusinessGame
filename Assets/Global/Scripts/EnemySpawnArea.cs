using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Threading;

public class EnemySpawnArea : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("Prefab of the enemy to spawn")]
    [SerializeField] private GameObject enemyPrefab;

    [Tooltip("Spawn interval in seconds")]
    [Range(0, 30)]
    [SerializeField] private float spawnInterval = 3f;

    [Tooltip("Maximum number of enemies that can be active at one time")]
    [SerializeField] private int maxSpawnedEnemies = 5;

    [Header("Spawn Area Settings")]
    [Tooltip("Reference to a Transform object representing the spawn area")]
    [SerializeField] private Transform spawnArea;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        // Clean up destroyed enemies from the list
        activeEnemies.RemoveAll(enemy => enemy == null);
        
        if (Time.time >= nextSpawnTime && activeEnemies.Count < maxSpawnedEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomPointInSpawnArea();

        // Ensure the spawn position is on the ground by raycasting downward
        if (spawnPosition != Vector3.zero)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            activeEnemies.Add(newEnemy);
        }
    }

    private Vector3 GetRandomPointInSpawnArea()
    {
        Vector3 areaCenter = spawnArea.position;
        Vector3 areaSize = spawnArea.localScale;

        // Randomize a point within the spawn area's bounds
        float xPos = Random.Range(-(areaSize.x / 2), (areaSize.x / 2));
        float zPos = Random.Range(-(areaSize.z / 2), (areaSize.z / 2));
        
        // You can adjust this if you want to spawn on a specific surface (e.g., ground level)
        // Debug.Log(new Vector3(areaCenter.x + xPos, areaCenter.y, areaCenter.z + zPos));
        return new Vector3(areaCenter.x + xPos, areaCenter.y, areaCenter.z + zPos);
    }

    private void RoomFinished() {
    }
}
