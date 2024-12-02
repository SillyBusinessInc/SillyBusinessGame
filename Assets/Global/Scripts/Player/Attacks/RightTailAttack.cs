using System.Linq; 
using UnityEngine;

[CreateAssetMenu(fileName = "TailAttacks", menuName = "RightTail")]
public class RightTailAttack : TailAttack
{
    [SerializeField]
    private int damage = 15;

    public override void Start()
    {
        base.Start();
        player.Tail.tailDoDamage = damage;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.animator;
        AnimationClip[] clips = animatorTailAttack.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.Where(x => x.name == "LeftTailAttack").Single();
        animatorTailAttack.speed *= (clip.length / duration) * player.Tail.increaseTailSpeed;
        animatorTailAttack.SetTrigger("RightAttack");
    }
}
