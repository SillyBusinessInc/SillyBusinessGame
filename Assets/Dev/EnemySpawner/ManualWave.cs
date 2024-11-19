using System.Linq;
using UnityEngine;

public class ManualWave : MonoBehaviour
{
    private int enemyCount = 0;
    // Changeable event in the Inspector
    [SerializeField]
    private Events waveDoneEvent = Events.WAVE_DONE;

    
    void Awake()
    {
        GlobalReference.SubscribeTo(Events.ENEMY_SPAWNED, OnEnemySpawned);
        GlobalReference.SubscribeTo(Events.ENEMY_KILLED, OnEnemyKilled);
    }

    void Start() {
        if (GameObject.FindGameObjectsWithTag("Enemy").Count() == 0) {
            GlobalReference.UnsubscribeTo(Events.ENEMY_SPAWNED, OnEnemySpawned);
            GlobalReference.UnsubscribeTo(Events.ENEMY_KILLED, OnEnemyKilled);
            InvokeEvent(waveDoneEvent);
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
        InvokeEvent(waveDoneEvent);
    }

    private void InvokeEvent(Events eventToInvoke)
    {
        GlobalReference.AttemptInvoke(eventToInvoke);
    }
}
