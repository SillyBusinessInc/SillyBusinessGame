using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnemiesNS;


public class Tail : MonoBehaviour
{
    public Player player;
    public BaseTail currentTail;
    public List<BaseTail> tails;

    public List<Attack> attacks;

    public Animator WaffleAnimator;

    public TailStatistic tailStatistic = new(); 

    [HideInInspector]
    public int attackIndex;

    [HideInInspector]
    public float tailDoDamage;
    [HideInInspector]
    public float cooldownTime,activeCooldownTime;
    [HideInInspector]
    public float activeResetComboTime;

    [HideInInspector]
    public bool tailCanDoDamage,flipCanDoDamage = false;
    
    [HideInInspector]
    public GameObject slamObject;
    [HideInInspector]
    public float slamObjectSize = 1.0f;

    public void Start()
    {
        // Initialize();
    }
    //leave this for the future
    // public void Initialize()
    // {
    //     tailStatistic.groundCombo = currentTail.groundCombo;
    //     tailStatistic.airCombo = currentTail.airCombo;
    // }

    //Let this stay here for the future
    // public void SwapTail()
    // {

    // }
    public void Update()
    {
        activeResetComboTime =
            player.currentState.GetType().Name != "AttackingState"
                ? activeResetComboTime + Time.deltaTime
                : 0.0f;
        if (activeResetComboTime >= tailStatistic.comboResetTime.GetValue())
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
        tailStatistic.flipTailDamage.AddMultiplier("WaffleQuakeDamageIncrease", 2f, true);
        tailStatistic.slamObjectSize.AddMultiplier("WaffleQuakeSizeIncrease", 1.5f, true);
        tailStatistic.flipTailCooldown.AddModifier("WaffleQuakeCooldownIncrease",3.0f);
    }

    public void ReverseWaffleQuake()
    {
        currentTail.groundCombo.Clear();
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "LeftTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "RightTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "FlipAttack").Single());
        tailStatistic.flipTailDamage.RemoveMultiplier("WaffleQuakeDamageIncrease", true);
        tailStatistic.slamObjectSize.RemoveMultiplier("WaffleQuakeSizeIncrease",true);
        tailStatistic.flipTailCooldown.RemoveModifier("WaffleQuakeCooldownIncrease");
    }
    public void DoubleTap()
    {
        currentTail.groundCombo.Clear();
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "LeftTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "RightTailAttack").Single());
        tailStatistic.increaseTailSpeed.AddMultiplier("DoubleTap",1.5f, true);
    }

    public void ReserseDoubleTap()
    {
        currentTail.groundCombo.Clear();
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "LeftTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "RightTailAttack").Single());
        currentTail.groundCombo.Add(attacks.Where(x => x.Name == "FlipAttack").Single());
        tailStatistic.increaseTailSpeed.RemoveMultiplier("DoubleTap", true);
    }
    public void OnTriggerEnter(Collider Collider)
    {
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
                if (Collider.GetComponent<EnemiesNS.EnemyBase>() != null)
                {
                    Collider.GetComponent<EnemiesNS.EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));
                }
            }
        }
    }

    
}
