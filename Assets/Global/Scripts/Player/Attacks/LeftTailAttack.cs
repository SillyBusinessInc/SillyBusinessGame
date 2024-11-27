using UnityEngine;

public class LeftTailAttack : TailAttack
{
    [SerializeField] private int damage = 10;
    
    public void Start()
    {
        player.Tail.tailDoDamage = damage;
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
