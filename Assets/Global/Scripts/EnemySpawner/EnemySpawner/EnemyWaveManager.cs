using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public List<Wave> waves;
    public List<Transform> spawnAreaTransforms;
    public GameObject spawnerObject;
    private int totalEnemies;
    private int deadEnemies;
    private int currentWave = 0;
    private int wavesDone = 0;
    private int maxWaves;
    private bool nextWave = false;
    public bool immediateStart = false;

    private void Start()
    {
        if (immediateStart){
            GlobalReference.AttemptInvoke(Events.WAVE_START);
        }
    }
    private void Update()
    {
        if (deadEnemies >= totalEnemies && nextWave)
        {
            WaveCompleted();
        }
    }

    //function that invokes wave start event

    [ContextMenu("Start Wave")]
    public void StartWaveTest()
    {
        GlobalReference.AttemptInvoke(Events.WAVE_START);
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
        nextWave = false;
        
        deadEnemies = 0;
        totalEnemies = 0;
        
        var spawner = spawnerObject.GetComponent<WaveSpawnArea>();
        totalEnemies = waves[currentWave].waveParts.Sum(wavePart => wavePart.enemyPrefabs.Sum(enemy => enemy.amount));
        nextWave = true;

        foreach (var wavePart in waves[currentWave].waveParts) // Access waveParts within each Wave
        {
            spawner.maxSpawnedEnemies = totalEnemies; // Use 'count' if that's the property name
            if (wavePart.spawnArea >= 0)
            {
                spawner.spawnArea = spawnAreaTransforms[wavePart.spawnArea];
                spawner.center = wavePart.center;
                foreach (var enemy in wavePart.enemyPrefabs) // Loop through each enemy in wavePart
                {
                    // Access the spawn area component and set the enemy properties
                    spawner.enemyPrefab = enemy.enemyPrefab;
                    spawner.waveDone = true;
                    foreach (var _ in Enumerable.Range(0, enemy.amount))
                    {
                        spawner.SpawnEnemy();
                        yield return new WaitForSeconds(waves[currentWave].interval);
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
        wavesDone++;
        if (wavesDone >= maxWaves)
        {
            GlobalReference.AttemptInvoke(Events.ALL_WAVES_DONE);
            nextWave = false;
            GlobalReference.AttemptInvoke(Events.NEXT_SPAWNER);
        }
        else
        {
            nextWave = true;
            currentWave++;
            GlobalReference.AttemptInvoke(Events.WAVE_START);
        }

    }
}