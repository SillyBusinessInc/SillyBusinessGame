using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MoldCoreSpawner : EnemyBase
{
    public float interfal = 1;
    public List<EnemyPrefabCount> enemyChanceList;
    // [SerializeField] private float health = 100;
    private int enemyCurrent = 0;
    public Transform spawnArea;
    private SingleEnemySpawnArea spawner = new SingleEnemySpawnArea();
    private float currentTime;
    
    void Start()
    {
        currentTime = Time.time;
        GlobalReference.AttemptInvoke(Events.MOLD_CORE_SPAWNED);
    }


    void OnDeath()
    {
        Destroy(gameObject);
        GlobalReference.AttemptInvoke(Events.MOLD_CORE_KILLED);
    }
    void Update()
    {
        if (Time.time >= interfal+currentTime)
        {
            currentTime = Time.time;
            SpawnEnemy();
        }

    }

    //spawn enemy function here with singleenemyspawner class
    public void SpawnEnemy()
    {
        spawner.SpawnEnemy(RandomDistribution.GetRandom(EnemyPrefabCount.GetDict(enemyChanceList)), spawnArea, false);
        GlobalReference.AttemptInvoke(Events.ENEMY_SPAWNED);
    }   

    // [ContextMenu("Test")]
    // public void test(){
    //     Debug.Log(RandomDistribution.GetRandom(EnemyPrefabCount.GetDict(enemyChanceList)));
    // }
}