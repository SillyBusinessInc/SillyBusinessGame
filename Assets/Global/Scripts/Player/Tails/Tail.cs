using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using EnemiesNS;


public class Tail : MonoBehaviour
{
    public Player player;
    public BaseTail currentTail;
    public List<BaseTail> tails;

    public List<Attack> attacks;

    public Animator WaffleAnimator;

    [HideInInspector]
    public int attackIndex;

    [HideInInspector]
    public float tailDoDamage;

    [HideInInspector]
    public float activeResetComboTime;

    public float comboResetTime = 2f;

    public float activeCooldownTime;
    public float cooldownTime = 0.0f;

    [HideInInspector]
    public bool tailCanDoDamage = false;
    public GameObject slamObject;
    public bool flipDoDamage = false;

    public float increaseTailSpeed = 1.0f;
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

        activeCooldownTime =
            player.currentState.GetType().Name != "AttackingState"
                ? activeCooldownTime + Time.deltaTime
                : activeCooldownTime;
    }

    public void WaffleQuake()
    {
        currentTail.groundCombo.Clear();
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "FlipAttack").Single());
        currentTail.groundCombo.First().damage *= 2;
        slamObject.transform.localScale *= 1.5f;;
        currentTail.groundCombo.Where(x => x.Name == "FlipAttack").Single().cooldown += 3f;
    }

    public void ReverseWaffleQuake()
    {
        currentTail.groundCombo.Clear();
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "LeftTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "RightTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "FlipAttack").Single());
        currentTail.groundCombo.Where(x => x.Name == "FlipAttack").Single().damage /= 2;
        slamObject.transform.localScale /= 1.5f;
        currentTail.groundCombo.Where(x => x.Name == "FlipAttack").Single().cooldown -= 3f;
    }
    public void DoubleTap()
    {
        currentTail.groundCombo.Clear();
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "LeftTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "RightTailAttack").Single());
        increaseTailSpeed = 1.5f;
    }

    public void ReserseDoubleTap()
    {
        currentTail.groundCombo.Clear();
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "LeftTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "RightTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "FlipAttack").Single());
        increaseTailSpeed = 1.0f;
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
