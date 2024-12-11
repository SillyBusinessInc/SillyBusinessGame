using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnemiesNS;
using Unity.VisualScripting;
using UnityEngine.AI;


public class Tail : MonoBehaviour
{
    public Player player;
    public BaseTail currentTail;
    public List<BaseTail> tails;
    public List<Attack> attacks;

    public TailStatistic tailStatistic = new();

    [HideInInspector] public int attackIndex;

    [HideInInspector] public float tailDoDamage;
    [HideInInspector] public float cooldownTime, activeCooldownTime;
    [HideInInspector] public float activeResetComboTime;

    [HideInInspector] public bool tailCanDoDamage, flipCanDoDamage = false;

    [HideInInspector] public GameObject slamObject;
    [HideInInspector] public float slamObjectSize = 1.0f;

    public void Start() { }

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



    public void OnTriggerEnter(Collider Collider)
    {
        if (!Collider.gameObject.CompareTag("Enemy") ||
            !tailCanDoDamage ||
            player.collidersEnemy.Contains(Collider) ||
            Collider.GetComponent<EnemiesNS.EnemyBase>() == null
        ) return;

        player.collidersEnemy.Add(Collider);
        float actualDamage = tailDoDamage * player.playerStatistic.AttackDamageMultiplier.GetValue();
        Collider.GetComponent<EnemiesNS.EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));


        // enemy 
        EnemiesNS.EnemyBase enemy = Collider.GetComponent<EnemiesNS.EnemyBase>();
        Debug.Log("Enemy: " + enemy);
        if (enemy != null)
        {
            enemy.agent.updatePosition = false;
            enemy.agent.updateRotation = false;
            enemy.agent.isStopped = true;

            Vector3 kb = -enemy.transform.forward * 25000;

            Rigidbody rb = enemy.TryGetComponent<Rigidbody>(out var rigidbody) ? rigidbody : enemy.AddComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(kb);
            }


        }

    }


}
