using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "FlipTail")]
public class FlipAttack : TailAttack
{
    public FlipAttack(string Name, float damage, float cooldown)
        : base(Name, damage, cooldown) { }

    public override void Start()
    {
        base.Start();
        player.Tail.slamObject.transform.localScale = new Vector3(3, 1, 3);
        player.Tail.slamObject.transform.localScale *=
            player.Tail.tailStatistic.slamObjectSize.GetValue();
        player.Tail.tailDoDamage = player.Tail.tailStatistic.flipTailDamage.GetValue();
        player.Tail.cooldownTime = player.Tail.tailStatistic.flipTailCooldown.GetValue();
        player.Tail.tailDoDamage *= player.Tail.tailStatistic.increaseDamage.GetValue();
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.WaffleAnimator;
        AnimationClip[] clips = animatorTailAttack.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.Where(x => x.name == "FlipAttack").Single();
        animatorTailAttack.speed *= clip.length / duration;
        animatorTailAttack.speed *= player.Tail.tailStatistic.increaseAttackSpeed.GetValue();
        animatorTailAttack.SetTrigger("FlipAttack");
    }

    public override IEnumerator SetStateIdle()
    {
        yield return new WaitForSeconds(duration / 2);
        player.Tail.flipCanDoDamage = true;
        player.Tail.slamObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        player.Tail.flipCanDoDamage = false;
        player.Tail.slamObject.SetActive(false);
        yield return new WaitForSeconds(duration / 2);
        player.SetState(player.states.Idle);
        player.Tail.cooldownTime = cooldown;
    }
}
