using UnityEngine;

public class LeftTailAttack : TailAttack
{
    public void Start()
    {
        player.tailDoDamage = player.Tail.leftTailDamage;
        player.tailCanDoDamage = true;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail
            .animator;
        animatorTailAttack.SetTrigger("LeftAttack");
        canDoDamage(0.5f);
    }
}
