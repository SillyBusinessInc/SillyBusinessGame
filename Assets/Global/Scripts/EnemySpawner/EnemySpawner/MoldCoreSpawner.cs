using UnityEngine;
using System.Collections.Generic;
using random = UnityEngine.Random;
using System.Collections;

[System.Serializable]
public class EnemyChances
{
    public GameObject enemyPrefab; // The enemy type
    public float chance;              // Number of this type to spawn
}

public class MoldCoreSpawner : MonoBehaviour
{
    public float interfal = 1;
    public List<EnemyPrefabCount> enemyChances;
    private float health = 100;

    //enemy spawn chance
    public List<GameObject> enemies;
    private int enemyCurrent = 0;
    public Transform spawnArea;
    private SingleEnemySpawnArea spawner = new SingleEnemySpawnArea();
    void Start()
    {
        GlobalReference.AttemptInvoke(Events.MOLD_CORE_SPAWNED);
        // StartCoroutine(SpawnEnemy());
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
        
    }

    //spawn enemy function here with singleenemyspawner class
    public IEnumerable SpawnEnemy()
    {
        // spawner.SpawnEnemy(enemyChances, spawnArea, false);
        GlobalReference.AttemptInvoke(Events.ENEMY_SPAWNED);
        Debug.Log("Enemy Spawned");
        yield return new WaitForSeconds(interfal);
    }   
}