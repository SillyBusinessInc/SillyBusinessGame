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
        player.Tail.tailDoDamage = damage;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.WaffleAnimator;
        AnimationClip[] clips = animatorTailAttack.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.Where(x => x.name == "RightTailAttack").Single();
        animatorTailAttack.speed *= (clip.length / duration) * player.Tail.increaseTailSpeed;
        animatorTailAttack.SetTrigger("RightAttack");

        player.playerAnimationsHandler.resetStates();
        player.playerAnimationsHandler.SetInt("AttackType", 0);
        player.playerAnimationsHandler.animator.SetTrigger("IsAttackingTrigger");
    }
}
