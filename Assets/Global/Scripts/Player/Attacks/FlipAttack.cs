using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "TailAttacks", menuName = "FlipTail")]
public class FlipAttack : TailAttack
{
    [SerializeField]
    private int damage = 20;

    public override void Start()
    {
        base.Start();
        player.Tail.tailDoDamage = damage;
        player.Tail.tailCanDoDamage = true;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.animator;
        AnimationClip[] clips = animatorTailAttack.runtimeAnimatorController.animationClips;
        AnimationClip clip = clips.Where(x => x.name == "LeftTailAttack").Single();
        animatorTailAttack.speed *= (clip.length / duration) * player.Tail.increaseTailSpeed;
        animatorTailAttack.SetTrigger("FlipAttack");
    }
}
