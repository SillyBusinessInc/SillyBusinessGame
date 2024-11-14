using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnemySpawnArea : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("Prefab of the enemy to spawn")]
    [SerializeField] public GameObject enemyPrefab;

    [SerializeField] public int maxSpawnedEnemies;

    [Header("Spawn Area Settings")]
    [SerializeField] public Transform spawnArea;

    private List<GameObject> activeEnemies = new List<GameObject>();

    public bool center = false;

    public bool waveDone = false;

    private void Update()
    {
        if(activeEnemies.Count == maxSpawnedEnemies && activeEnemies.TrueForAll(enemy => enemy == null) &&waveDone)
        {
            waveDone = false;
            GlobalReference.AttemptInvoke(Events.WAVE_DONE);
            Debug.Log("All enemies are dead");
        }
    }


    public void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomPointInSpawnArea();

        // Ensure the spawn position is on the ground by raycasting downward
        if (spawnPosition != Vector3.zero)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            activeEnemies.Add(newEnemy);
            GlobalReference.AttemptInvoke(Events.ENEMY_SPAWNED);

        }
    }

    private Vector3 GetRandomPointInSpawnArea()
    {
        Vector3 areaCenter = spawnArea.position;
        Vector3 areaSize = spawnArea.localScale;

        if (center)
        {
            return areaCenter;
        }
        // Randomize a point within the spawn area's bounds
        float xPos = Random.Range(-(areaSize.x / 2), (areaSize.x / 2));
        float zPos = Random.Range(-(areaSize.z / 2), (areaSize.z / 2));
        
        // You can adjust this if you want to spawn on a specific surface (e.g., ground level)
        return new Vector3(areaCenter.x + xPos, areaCenter.y, areaCenter.z + zPos);
    }
}
