using UnityEngine;

public class AttackCaller : MonoBehaviour
{
    void PlayerAttackStarted()
    {
        GlobalReference.AttemptInvoke(Events.PLAYER_ATTACK_STARTED);
    }

    void PlayerAttackEnded()
    {
        GlobalReference.AttemptInvoke(Events.PLAYER_ATTACK_ENDED);
    }
}
