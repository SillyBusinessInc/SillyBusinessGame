using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "RightTail")]
public class RightTailAttack : TailAttack
{
    public RightTailAttack(string Name, float damage, float cooldown)
        : base(Name, damage, cooldown) { }

    public override void Start()
    {
        Debug.Log("Right Tail Attack first");
        base.Start();
        player.Tail.tailCanDoDamage = true;
        player.Tail.tailDoDamage = player.Tail.tailStatistic.rightTailDamage.GetValue();
        player.Tail.tailDoDamage *= player.playerStatistic.AttackDamageMultiplier.GetValue();
        player.Tail.cooldownTime = player.Tail.tailStatistic.rightTailCooldown.GetValue();
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.WaffleAnimator;
        AnimationClip[] clips = animatorTailAttack.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.Where(x => x.name == "RightTailAttack").Single();
        animatorTailAttack.speed *= (clip.length / duration);
        animatorTailAttack.speed *= player.Tail.tailStatistic.increaseTailSpeed.GetValue();
        Debug.Log(player.playerStatistic.AttackSpeedMultiplier.GetValue());
        animatorTailAttack.speed *= player.playerStatistic.AttackSpeedMultiplier.GetValue();
        animatorTailAttack.SetTrigger("RightAttack");

        player.playerAnimationsHandler.resetStates();
        player.playerAnimationsHandler.SetInt("AttackType", 0);
        player.playerAnimationsHandler.animator.SetTrigger("IsAttackingTrigger");
    }
}
