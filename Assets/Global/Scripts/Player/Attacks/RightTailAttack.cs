using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "RightTail")]
public class RightTailAttack : TailAttack
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
        AnimationClip clip = clips.Where(x => x.name == "RightTailAttack").Single();
        animatorTailAttack.speed *= (clip.length / duration) * player.Tail.increaseTailSpeed;
        animatorTailAttack.SetTrigger("RightAttack");
    }
}
