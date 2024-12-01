using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "LeftTail")]
public class LeftTailAttack : TailAttack
{
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
        AnimationClip clip = clips.Where(x => x.name == "LeftTailAttack").Single();
        animatorTailAttack.speed *= (clip.length / duration) * player.Tail.increaseTailSpeed; 
        animatorTailAttack.SetTrigger("LeftAttack");
    }
}
