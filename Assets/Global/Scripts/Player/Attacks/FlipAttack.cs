using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "FlipTail")]
public class FlipAttack : TailAttack
{
    public FlipAttack(string Name, float damage, float cooldown)
        : base(Name, damage, cooldown) { }

    public override void Start()
    {
        base.Start();
        player.Tail.tailDoDamage = damage;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.WaffleAnimator;
        AnimationClip[] clips = animatorTailAttack.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.Where(x => x.name == "FlipAttack").Single();
        animatorTailAttack.speed *= clip.length / duration;
        animatorTailAttack.SetTrigger("FlipAttack");
        player.playerAnimationsHandler.resetStates();
        player.playerAnimationsHandler.SetInt("AttackType", 2);
        player.playerAnimationsHandler.animator.SetTrigger("IsAttackingTrigger");
    }
    public override IEnumerator SetStateIdle()
    {
        yield return new WaitForSeconds(duration / 2);
        player.Tail.flipDoDamage = true;
        player.Tail.slamObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        player.Tail.flipDoDamage = false;
        player.Tail.slamObject.SetActive(false);
        yield return new WaitForSeconds(duration / 2);
        player.SetState(player.states.Idle);
        player.Tail.cooldownTime = cooldown;
    }
}
