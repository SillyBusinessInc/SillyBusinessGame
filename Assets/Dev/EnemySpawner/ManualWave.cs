using System.Linq;
using UnityEngine;

public class ManualWave : MonoBehaviour
{
    private int enemyCount = 0;
    
    void Awake()
    {
        GlobalReference.SubscribeTo(Events.ENEMY_SPAWNED, OnEnemySpawned);
        GlobalReference.SubscribeTo(Events.ENEMY_KILLED, OnEnemyKilled);
    }

    void Start() {
        if (GameObject.FindGameObjectsWithTag("Enemy").Count() == 0) {
            GlobalReference.UnsubscribeTo(Events.ENEMY_SPAWNED, OnEnemySpawned);
            GlobalReference.UnsubscribeTo(Events.ENEMY_KILLED, OnEnemyKilled);
            GlobalReference.AttemptInvoke(Events.WAVE_DONE);
        }
    }

    private void OnEnemySpawned() {
        enemyCount++;
    }

    private void OnEnemyKilled() {
        enemyCount --;
        if (enemyCount <= 0) OnEndWave();
    }

    private void OnEndWave() {
        GlobalReference.UnsubscribeTo(Events.ENEMY_SPAWNED, OnEnemySpawned);
        GlobalReference.UnsubscribeTo(Events.ENEMY_KILLED, OnEnemyKilled);
        GlobalReference.AttemptInvoke(Events.WAVE_DONE);
    }
}
