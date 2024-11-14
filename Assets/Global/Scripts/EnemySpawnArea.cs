using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnemySpawnArea : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("Prefab of the enemy to spawn")]
    [SerializeField] public GameObject enemyPrefab;

    [Tooltip("Spawn interval in seconds")]
    [Range(0, 30)]
    [SerializeField] public float spawnInterval = 1f;

    [Tooltip("Maximum number of enemies that can be active at one time")]
    [SerializeField] public int maxSpawnedEnemies = 5;

    [Header("Spawn Area Settings")]
    [Tooltip("Reference to a Transform object representing the spawn area")]
    [SerializeField] public Transform spawnArea;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private float nextSpawnTime;

    private bool startSpawning = false;
    public bool center = false;

    public bool waveDone = false;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime && activeEnemies.Count < maxSpawnedEnemies &&startSpawning)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }else if(activeEnemies.Count == maxSpawnedEnemies && activeEnemies.TrueForAll(enemy => enemy == null) &&waveDone)
        {
            waveDone = false;
            GlobalReference.AttemptInvoke(Events.WAVE_DONE);
            Debug.Log("All enemies are dead");
        }
    }

    //function for how many died vs the max spawned enemies like (1/5) it needs to be string
    [ContextMenu("Get Enemy Count")]
    public string GetEnemyCount()
    {
        int deadEnemies = 0;
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy == null)
            {
                deadEnemies++;
            }
        }
        Debug.Log(deadEnemies + "/" + activeEnemies.Count);
        return deadEnemies + "/" + activeEnemies.Count;
    }

    //listen to event
    private void Awake()
    {
        GlobalReference.SubscribeTo(Events.SPAWN_WAVE, startWaveInitials);
    }

    //stop listening to event
    private void OnDestroy()
    {
        GlobalReference.UnsubscribeTo(Events.SPAWN_WAVE, startWaveInitials);
    }

    public void startWaveInitials()
    {
        activeEnemies.Clear();
        waveDone = true;
        startSpawning = true;
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
        // Debug.Log(new Vector3(areaCenter.x + xPos, areaCenter.y, areaCenter.z + zPos));
        return new Vector3(areaCenter.x + xPos, areaCenter.y, areaCenter.z + zPos);
    }
}
