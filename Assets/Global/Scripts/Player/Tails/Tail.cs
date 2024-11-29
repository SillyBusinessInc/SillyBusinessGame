using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Tail : MonoBehaviour
{
    public Player player;
    public BaseTail currentTail;
    public List<BaseTail> tails;

    public List<BaseTail> defaultTails;
    public List<Attack> attacks;

    public List<Attack> defaultAttacks;
    public Animator animator;

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

    public void Start()
    {
        WaffleQuake();
    }

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
        slamObject.transform.localScale *= 1.5f;
        //cooldownTime += 3.0f;
    }

    public void ReverseWaffleQuake()
    {
        currentTail.groundCombo = defaultTails
            .Where(x => x.Name == "DefaultWaffle")
            .Single()
            .groundCombo;
        slamObject.transform.localScale /= 1.5f;
        //cooldownTime -= 3.0f;
    }

    public void DoubleTap()
    {
        currentTail.groundCombo.Clear();
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "LeftTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "RightTailAttack").Single());
        increaseTailSpeed = 1.5f;
    }

    public void ReverseDoubleTap()
    {
        currentTail.groundCombo = defaultTails
            .Where(x => x.Name == "DefaultWaffle")
            .Single()
            .groundCombo;
        increaseTailSpeed = 1.0f;
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
