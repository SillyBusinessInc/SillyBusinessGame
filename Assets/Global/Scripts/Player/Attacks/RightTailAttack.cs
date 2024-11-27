using UnityEngine;

public class RightTailAttack : TailAttack
{
    public void Start()
    {
        player.Tail.tailDoDamage = player.Tail.rightTailDamage;
        player.tailCanDoDamage = true;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail
            .animator;
        animatorTailAttack.SetTrigger("RightAttack");
        canDoDamage(0.5f);
    }
}
