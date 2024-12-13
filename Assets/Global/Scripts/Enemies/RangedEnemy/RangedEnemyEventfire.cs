using UnityEngine;

public class RangedEnemyEventfire : MonoBehaviour
{
    void OnFireBullet()
    {
        // Debug.Log("Firing Bullet");
        GlobalReference.AttemptInvoke(Events.ENEMY_ATTACK_STARTED);
    }

    void OnAttackEnd()
    {
        // Debug.Log("Attack Ended");

        GlobalReference.AttemptInvoke(Events.ENEMY_ATTACK_ENDED);
    }
    void OnIdleEnd()
    {
        // Debug.Log("Idle Ended");
        GlobalReference.AttemptInvoke(Events.ENEMY_TO_IDLE);
    }
}