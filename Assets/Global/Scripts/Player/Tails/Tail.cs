using System;
using UnityEngine;
using EnemiesNS;

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

    [HideInInspector]
    public bool tailCanDoDamage = false;
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
        Debug.Log($"Hit: {Collider}", Collider.gameObject);
        if (Collider.gameObject.CompareTag("Enemy"))
        {
            if (tailCanDoDamage)
            {
                if (player.collidersEnemy.Contains(Collider))
                {
                    return;
                }
                player.collidersEnemy.Add(Collider);
                float actualDamage =
                    tailDoDamage * player.playerStatistic.AttackDamageMultiplier.GetValue();
                Debug.Log($"Going to do {actualDamage} points of damage");
                if (Collider.GetComponent<EnemiesNS.EnemyBase>() != null)
                {
                    Collider.GetComponent<EnemiesNS.EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));
                }
            }
        }
    }
}
