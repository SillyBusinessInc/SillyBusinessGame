using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public List<Wave> waves;
    // public List<GameObject> enemyPrefabs;
    public List<Transform> spawnAreaTransforms;
    public GameObject spawnArea;
    // public int enemiesPerWave = 5;
    private int totalEnemies;
    private int deadEnemies;

    //serialized for testing
    [SerializeField]private int currentWave = 0;

    //serialized for testing
    [SerializeField]private int wavesDone = 0;
    public int maxWaves;
    private bool nextWave = true;

    private void Start()
    {
        GlobalReference.AttemptInvoke(Events.WAVE_START);
    }
    private void Update()
    {
        if (deadEnemies >= totalEnemies && nextWave)
        {
            WaveCompleted();
            // Debug.Log("test");
        }
    }
    private void Awake()
    {
        maxWaves = waves.Count;
        GlobalReference.SubscribeTo(Events.WAVE_START, StartWave);
        GlobalReference.SubscribeTo(Events.WAVE_DONE, WaveCompleted);
        GlobalReference.SubscribeTo(Events.ENEMY_KILLED, OnEnemyDeath);
        GlobalReference.SubscribeTo(Events.ENEMY_SPAWNED, () => totalEnemies++);
    }

    private void OnDestroy()
    {
        GlobalReference.UnsubscribeTo(Events.WAVE_START, StartWave);
        GlobalReference.UnsubscribeTo(Events.WAVE_DONE, WaveCompleted);
        GlobalReference.UnsubscribeTo(Events.ENEMY_KILLED, OnEnemyDeath);
        GlobalReference.UnsubscribeTo(Events.ENEMY_SPAWNED, () => totalEnemies++);
    }
    public void StartWave()
    {
        nextWave = false;
        currentWave++;
        deadEnemies = 0;
        //count all the enemies in the wave part
        //take a random spawn area
        var spawner = spawnArea.GetComponent<EnemySpawnArea>();

        foreach (var wave in waves) // Loop through each wave in the list
        {
            foreach (var wavePart in wave.waveParts) // Access waveParts within each Wave
            {
                totalEnemies = wavePart.enemyPrefabs.Sum(enemy => enemy.amount);
                Debug.Log("Total enemies: " + totalEnemies);
                Debug.Log("Spawn area: " + wavePart.spawnArea);
                if (wavePart.spawnArea >= 0)
                {
                    Debug.Log("ghellooooo");
                    spawner.spawnArea = spawnAreaTransforms[wavePart.spawnArea];
                    foreach (var enemy in wavePart.enemyPrefabs) // Loop through each enemy in wavePart
                    {
                        Debug.Log("ghiiiiiii");
                        // Access the spawn area component and set the enemy properties
                        spawner.enemyPrefab = enemy.enemyPrefab;
                        spawner.maxSpawnedEnemies = enemy.amount; // Use 'count' if that's the property name
                        spawner.startWaveInitials();
                        // GlobalReference.AttemptInvoke(Events.SPAWN_WAVE);
                    }
                }
                else
                {
                    Debug.LogError("No spawn area found");
                }
            }
        }

       
    }

    private void OnEnemyDeath()
    {
        deadEnemies++;
        if (deadEnemies >= totalEnemies)
        {
            WaveCompleted();
        }
    }

    private void WaveCompleted()
    {   
        totalEnemies = 0;
        if (wavesDone >= maxWaves)
        {
            GlobalReference.AttemptInvoke(Events.ALL_WAVES_DONE);
            nextWave = false;
        }
        else
        {
            Debug.Log("Wave Completed");
            GlobalReference.AttemptInvoke(Events.WAVE_START);
            nextWave = true;
        }
        wavesDone++;

    }
}