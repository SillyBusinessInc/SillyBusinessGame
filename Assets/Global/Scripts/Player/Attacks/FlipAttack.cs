using UnityEngine;

public class FlipAttack : TailAttack
{
    public void Start()
    {
        player.Tail.tailDoDamage = player.Tail.flipDamage;
        player.tailCanDoDamage = true;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail
            .animator;
        animatorTailAttack.SetTrigger("FlipAttack");
        canDoDamage(0.5f);
    }
}
