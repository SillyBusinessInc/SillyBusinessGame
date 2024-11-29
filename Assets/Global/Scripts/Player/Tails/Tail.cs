using System;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public Player player;
    public BaseTail currentTail;

    public List<BaseTail> tails;
    public List<Attack> attacks;
    public Animator animator;

    [HideInInspector]
    public int attackIndex;

    [HideInInspector]
    public int tailDoDamage;

    [HideInInspector]
    public float activeResetComboTime;

    public float comboResetTime = 2f;

    [HideInInspector]
    public bool tailCanDoDamage = false;
    public GameObject slamObject;
    public bool flipDoDamage = false;

    public void Update()
    {
        activeResetComboTime =
            player.currentState.GetType().Name != "AttackingState"
                ? activeResetComboTime + Time.deltaTime
                : 0.0f;
        if (activeResetComboTime >= comboResetTime)
        {
            attackIndex = 0;
            activeResetComboTime = 0.0f;
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
            if (tailCanDoDamage)
            {
                float actualDamage =
                    tailDoDamage * player.playerStatistic.AttackDamageMultiplier.GetValue();
                if (Collider.GetComponent<EnemyBase>() != null)
                {
                    Collider.GetComponent<EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));
                }
            }
        }
    }
}
