using UnityEngine;

public class FlipAttack : TailAttack
{
    [SerializeField] private int damage = 20;
 
    public void Start()
    {
        player.Tail.tailDoDamage = damage;
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
