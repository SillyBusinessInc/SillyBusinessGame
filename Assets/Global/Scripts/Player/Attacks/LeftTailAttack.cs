using UnityEngine;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "LeftTail")]
public class LeftTailAttack : TailAttack
{
    [SerializeField] private int damage = 10;
    
    public override void Start()
    {
        base.Start();
        player.Tail.tailDoDamage = damage;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail
            .animator;
        animatorTailAttack.SetTrigger("LeftAttack");

        player.playerAnimationsHandler.resetStates();
        player.playerAnimationsHandler.SetInt("AttackType", 1);
        player.playerAnimationsHandler.animator.SetTrigger("IsAttackingTrigger");
    }
}
