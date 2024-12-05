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
        base.Start();
        player.Tail.tailCanDoDamage = true;
        player.Tail.tailDoDamage = player.Tail.tailStatistic.rightTailDamage.GetValue();
        player.Tail.cooldownTime = player.Tail.tailStatistic.rightTailCooldown.GetValue();
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.WaffleAnimator;
        AnimationClip[] clips = animatorTailAttack.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.Where(x => x.name == "RightTailAttack").Single();
        animatorTailAttack.speed *= (clip.length / duration) * player.Tail.tailStatistic.increaseTailSpeed.GetValue();
        animatorTailAttack.SetTrigger("RightAttack");
    }
}
