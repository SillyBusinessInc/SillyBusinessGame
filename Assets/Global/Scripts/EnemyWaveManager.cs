using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public List<Wave> waves;
    public List<Transform> spawnAreaTransforms;
    public GameObject spawnArea;
    private int totalEnemies;
    private int deadEnemies;

    //serialized for testing
    [SerializeField]private int currentWave = 0;

    //serialized for testing
    [SerializeField]private int wavesDone = 0;
    public int maxWaves;
    private bool nextWave = true;
    public bool immediateStart = false;

    private void Start()
    {
        if (immediateStart){
            GlobalReference.AttemptInvoke(Events.WAVE_START);
        }
    }
    private void Update()
    {
        // Debug.Log(deadEnemies+"/"+totalEnemies);
        if (deadEnemies >= totalEnemies && nextWave && immediateStart)
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
    }

    private void OnDestroy()
    {
        GlobalReference.UnsubscribeTo(Events.WAVE_START, () => StartCoroutine(StartWave()));
        GlobalReference.UnsubscribeTo(Events.WAVE_DONE, WaveCompleted);
        GlobalReference.UnsubscribeTo(Events.ENEMY_KILLED, OnEnemyDeath);
    }
    public IEnumerator StartWave()
    {
        Debug.Log("Starting wave");
        nextWave = false;
        immediateStart = true;
        
        deadEnemies = 0;
        totalEnemies = 0;
        
        var spawner = spawnArea.GetComponent<WaveSpawnArea>();
        totalEnemies = waves[currentWave].waveParts.Sum(wavePart => wavePart.enemyPrefabs.Sum(enemy => enemy.amount));

        foreach (var wavePart in waves[currentWave].waveParts) // Access waveParts within each Wave
        {
            spawner.maxSpawnedEnemies = totalEnemies; // Use 'count' if that's the property name
            if (wavePart.spawnArea >= 0)
            {
                spawner.spawnArea = spawnAreaTransforms[wavePart.spawnArea];
                foreach (var enemy in wavePart.enemyPrefabs) // Loop through each enemy in wavePart
                {
                    // Access the spawn area component and set the enemy properties
                    spawner.enemyPrefab = enemy.enemyPrefab;
                    spawner.waveDone = true;
                    foreach (var _ in Enumerable.Range(0, enemy.amount))
                    {
                        spawner.SpawnEnemy();
                        yield return new WaitForSeconds(waves[currentWave].interfal);
                    }
                }
            }
            else
            {
                Debug.LogError("No spawn area found");
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
            nextWave = true;
            currentWave++;
            GlobalReference.AttemptInvoke(Events.WAVE_START);
        }
        wavesDone++;

    }
}