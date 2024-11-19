using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MoldCoreSpawner : MonoBehaviour
{
    public float interfal = 1;
    public List<EnemyPrefabCount> enemyChanceList;
    private float health = 100;
    private int enemyCurrent = 0;
    public Transform spawnArea;
    private SingleEnemySpawnArea spawner = new SingleEnemySpawnArea();
    public float currentTime;
    
    void Start()
    {
        currentTime = Time.time;
        GlobalReference.AttemptInvoke(Events.MOLD_CORE_SPAWNED);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health -= 10;
            Debug.Log(health);
            if (health <= 0)
            {
                OnDeath();
            }
        }
    }

    void OnDeath()
    {
        Destroy(gameObject);
        GlobalReference.AttemptInvoke(Events.MOLD_CORE_KILLED);
        Debug.Log("All enemies are dead");
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
        Debug.Log("Enemy Spawned");
    }   

    [ContextMenu("Test")]
    public void test(){
        Debug.Log(RandomDistribution.GetRandom(EnemyPrefabCount.GetDict(enemyChanceList)));
    }
}