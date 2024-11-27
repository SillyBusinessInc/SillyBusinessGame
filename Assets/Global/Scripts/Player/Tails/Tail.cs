using System;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public Player player;
    public BaseTail currentTail;
    public Animator animator;
    
    [HideInInspector]
    public int attackIndex;

    [HideInInspector]
    public int tailDoDamage;

    public void ChangeTail(BaseTail newtail, Animator animator)
    {
        currentTail = newtail;
        this.animator = animator;
    }

    public void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.CompareTag("Enemy"))
        {
            if (player.collidersEnemy.Contains(Collider))
            {
                return;
            }
            player.collidersEnemy.Add(Collider);
            if (player.tailCanDoDamage)
            {
                float actualDamage =
                    tailDoDamage * player.playerStatistic.AttackDamageMultiplier.GetValue();
                Collider.GetComponent<EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));
            }
        }
    }
}
