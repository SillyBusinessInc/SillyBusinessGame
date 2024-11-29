using System.Collections;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "FlipTail")]
public class FlipAttack : TailAttack
{

    public override string Name => "FlipAttack";

    public override void Start()
    {
        base.Start();
        player.Tail.tailDoDamage = damage;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.animator;
        AnimationClip[] clips = animatorTailAttack.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.Where(x => x.name == "FlipAttack").Single();
        animatorTailAttack.speed *= clip.length / duration;
        animatorTailAttack.SetTrigger("FlipAttack");
    }

    public override IEnumerator SetStateIdle()
    {
        // TODO: duration is  the base speed of the attack, and so it should work with the duration of the animation and stuff.
        // And also with attack speed
        yield return new WaitForSeconds(duration / 2);
        player.Tail.flipDoDamage = true;
        player.Tail.slamObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        player.Tail.flipDoDamage = false;
        player.Tail.slamObject.SetActive(false);
        yield return new WaitForSeconds(duration / 2);
        player.SetState(player.states.Idle);
    }
}