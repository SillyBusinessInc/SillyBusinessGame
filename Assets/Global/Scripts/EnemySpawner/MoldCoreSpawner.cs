using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MoldCoreSpawner : EnemyBase
{
    public float interfal = 1;
    public List<EnemyPrefabCount> enemyChanceList;
    public Transform spawnArea;
    private SingleEnemySpawnArea spawner = new SingleEnemySpawnArea();
    private float currentTime;
    [SerializeField]private int limit = 10;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    List<GameObject> toRemove = new();

    
    new void Start()
    {
        base.Start();
        currentTime = Time.time;
        GlobalReference.AttemptInvoke(Events.MOLD_CORE_SPAWNED);
    }


    public override void OnDeath()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        Destroy(gameObject);
        GlobalReference.AttemptInvoke(Events.MOLD_CORE_KILLED);
        GlobalReference.AttemptInvoke(Events.NEXT_SPAWNER);
        
    }

    void Update()
    {
        
        if (Time.time >= interfal+currentTime && spawnedEnemies.Count < limit)
        {            
            currentTime = Time.time;
            SpawnEnemy();
        }
        if (health <= 0)
        {
            OnDeath();
        }
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy == null)
            {
                toRemove.Add(enemy);
            }
        }
        foreach (GameObject enemy in toRemove)
        {
            spawnedEnemies.Remove(enemy);
            currentTime = Time.time;
        }
        toRemove.Clear();

    }

    //spawn enemy function here with singleenemyspawner class
    public void SpawnEnemy()
    {   
        GameObject enemy = spawner.SpawnEnemy(RandomDistribution.GetRandom(EnemyPrefabCount.GetDict(enemyChanceList)), spawnArea, false);
        if (enemy != null)
        {
            spawnedEnemies.Add(enemy);
            GlobalReference.AttemptInvoke(Events.ENEMY_SPAWNED);
        }
    }   
}