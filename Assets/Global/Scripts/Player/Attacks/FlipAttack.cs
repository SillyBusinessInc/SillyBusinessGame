using UnityEngine;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "FlipTail")]
public class FlipAttack : TailAttack
{
    [SerializeField] private int damage = 20;
 
    public override void Start()
    {
        base.Start();
        player.Tail.tailDoDamage = damage;
        player.Tail.tailCanDoDamage = true;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail
            .animator;
        animatorTailAttack.SetTrigger("FlipAttack");

        player.playerAnimationsHandler.resetStates();
        player.playerAnimationsHandler.SetInt("AttackType", 2);
        player.playerAnimationsHandler.animator.SetTrigger("IsAttackingTrigger");

    }
}
