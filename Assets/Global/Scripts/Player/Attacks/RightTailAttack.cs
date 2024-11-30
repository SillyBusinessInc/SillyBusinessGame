using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "TailAttacks", menuName = "RightTail")]
public class RightTailAttack : TailAttack
{
    [SerializeField] private int damage = 15;
    
    public override void Start()
    {
        Debug.Log("Right Tail Attack first");
        base.Start();
        player.Tail.tailDoDamage = damage;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail
            .animator;
        animatorTailAttack.SetTrigger("RightAttack");

        player.playerAnimationsHandler.resetStates();
        player.playerAnimationsHandler.SetInt("AttackType", 0);
        player.playerAnimationsHandler.animator.SetTrigger("IsAttackingTrigger");
    }
}
