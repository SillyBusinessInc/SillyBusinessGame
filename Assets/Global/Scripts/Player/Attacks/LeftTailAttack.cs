using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "LeftTail")]
public class LeftTailAttack : TailAttack
{
    public override string Name => "LeftTailAttack";

    public override void Start()
    {
        base.Start();
        player.Tail.tailCanDoDamage = true;
        player.Tail.tailDoDamage = damage;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.animator;
        AnimationClip[] clips = animatorTailAttack.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.Where(x => x.name == "FlipAttack").Single();
        animatorTailAttack.speed *= (clip.length / duration) * player.Tail.increaseTailSpeed; 
        animatorTailAttack.SetTrigger("LeftAttack");
    }
}
