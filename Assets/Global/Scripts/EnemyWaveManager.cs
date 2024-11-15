using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public List<Transform> spawnAreaTransforms;
    public GameObject spawnArea;
    public int enemiesPerWave = 5;
    private int totalEnemies;
    private int deadEnemies;

    //serialized for testing
    [SerializeField]private int currentWave = 0;

    //serialized for testing
    [SerializeField]private int wavesDone = 0;
    public int maxWaves = 3;
    private bool nextWave = true;

    // private void Start()
    // {
    //     StartWave();
    // }
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
        totalEnemies = enemiesPerWave;
        //take a random spawn area

        
        spawnArea.GetComponent<EnemySpawnArea>().enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        spawnArea.GetComponent<EnemySpawnArea>().spawnArea = spawnAreaTransforms[Random.Range(0, spawnAreaTransforms.Count)];
        spawnArea.GetComponent<EnemySpawnArea>().maxSpawnedEnemies = enemiesPerWave;           
        GlobalReference.AttemptInvoke(Events.SPAWN_WAVE);
        
       
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
        if (wavesDone >= maxWaves)
        {
            GlobalReference.AttemptInvoke(Events.ALL_WAVES_DONE);
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