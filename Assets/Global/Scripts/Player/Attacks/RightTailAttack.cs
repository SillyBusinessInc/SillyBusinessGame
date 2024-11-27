using UnityEngine;

public class RightTailAttack : TailAttack
{
    [SerializeField] private int damage = 15;
    
    public void Start()
    {
        player.Tail.tailDoDamage = damage;
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
