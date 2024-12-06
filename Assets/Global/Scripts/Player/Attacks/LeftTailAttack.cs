using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "LeftTail")]
public class LeftTailAttack : TailAttack
{
    public LeftTailAttack(string Name, float damage, float cooldown)
        : base(Name, damage, cooldown) { }

    public override void Start()
    {
        base.Start();
        player.Tail.tailCanDoDamage = true;
        player.Tail.tailDoDamage = player.Tail.tailStatistic.leftTailDamage.GetValue();
        player.Tail.tailDoDamage *= player.Tail.tailStatistic.increaseDamage.GetValue();
        player.Tail.cooldownTime = player.Tail.tailStatistic.leftTailCooldown.GetValue();
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.WaffleAnimator;
        AnimationClip[] clips = animatorTailAttack.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.Where(x => x.name == "LeftTailAttack").Single();
        animatorTailAttack.speed *= (clip.length / duration);
        animatorTailAttack.speed *= player.Tail.tailStatistic.increaseTailSpeed.GetValue();
        animatorTailAttack.speed *= player.Tail.tailStatistic.increaseAttackSpeed.GetValue();
        animatorTailAttack.SetTrigger("LeftAttack");
    }
}
