using System;
using Unity.VisualScripting;
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

    [HideInInspector]
    public float activeAttackCooldown;
    public float attackResettingTime = 2f;

    public void Update()
    {
        activeAttackCooldown =
            player.currentState.GetType().Name != "AttackingState"
                ? activeAttackCooldown + Time.deltaTime
                : 0.0f;
        if (activeAttackCooldown >= attackResettingTime)
        {
            attackIndex = 0;
            activeAttackCooldown = 0.0f;
        }
    }

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
