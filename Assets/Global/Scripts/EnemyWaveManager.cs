using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
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
        // Debug.Log(deadEnemies+"/"+totalEnemies);
        if (deadEnemies >= totalEnemies && nextWave)
        {
            WaveCompleted();
            // Debug.Log("test");
        }
    }
    private void Awake()
    {
        maxWaves = waves.Count;
        GlobalReference.SubscribeTo(Events.WAVE_START, () => StartCoroutine(StartWave()));
        GlobalReference.SubscribeTo(Events.WAVE_DONE, WaveCompleted);
        GlobalReference.SubscribeTo(Events.ENEMY_KILLED, OnEnemyDeath);
        // GlobalReference.SubscribeTo(Events.ENEMY_SPAWNED, () => totalEnemies++);
    }

    private void OnDestroy()
    {
        GlobalReference.UnsubscribeTo(Events.WAVE_START, () => StartCoroutine(StartWave()));
        GlobalReference.UnsubscribeTo(Events.WAVE_DONE, WaveCompleted);
        GlobalReference.UnsubscribeTo(Events.ENEMY_KILLED, OnEnemyDeath);
        // GlobalReference.UnsubscribeTo(Events.ENEMY_SPAWNED, () => totalEnemies++);
    }
    public IEnumerator StartWave()
    {
        Debug.Log("Starting wave");
        nextWave = false;
        
        deadEnemies = 0;
        totalEnemies = 0;
        //count all the enemies in the wave part
        //take a random spawn area
        var spawner = spawnArea.GetComponent<EnemySpawnArea>();
        totalEnemies = waves[currentWave].waveParts.Sum(wavePart => wavePart.enemyPrefabs.Sum(enemy => enemy.amount));

        foreach (var wavePart in waves[currentWave].waveParts) // Access waveParts within each Wave
        {
            Debug.Log("Total enemies: " + totalEnemies);
            Debug.Log("Spawn area: " + wavePart.spawnArea);
            spawner.maxSpawnedEnemies = totalEnemies; // Use 'count' if that's the property name
            if (wavePart.spawnArea >= 0)
            {
                Debug.Log("ghellooooo");
                spawner.spawnArea = spawnAreaTransforms[wavePart.spawnArea];
                foreach (var enemy in wavePart.enemyPrefabs) // Loop through each enemy in wavePart
                {
                    Debug.Log("ghiiiiiii");
                    // Access the spawn area component and set the enemy properties
                    spawner.enemyPrefab = enemy.enemyPrefab;
                    spawner.waveDone = true;
                    foreach (var _ in Enumerable.Range(0, enemy.amount))
                    {
                        spawner.SpawnEnemy();
                        yield return new WaitForSeconds(1);
                    }
                    // spawner.startWaveInitials();
                    // GlobalReference.AttemptInvoke(Events.SPAWN_WAVE);
                }
            }
            else
            {
                Debug.LogError("No spawn area found");
            }
        }
        Debug.Log("Wave ended");
       
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
            nextWave = true;
            currentWave++;
            GlobalReference.AttemptInvoke(Events.WAVE_START);
        }
        wavesDone++;

    }
}