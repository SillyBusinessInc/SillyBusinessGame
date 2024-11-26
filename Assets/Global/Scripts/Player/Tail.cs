using System;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
public class Tail : MonoBehaviour
{
    public Player player;

    public List<GameObject> groundCombo = new();
    public List<GameObject> airCombo = new(); 
    [HideInInspector]
    public List<GameObject> currentCombo = new();
    public Animator animatorLeftAttack;
    public Animator animatorRightAttack;

    public void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.CompareTag("Enemy"))
        {
            if(player.tailCanDoDamage)
            {
                float actualDamage = player.tailDoDamage * player.playerStatistic.AttackDamageMultiplier.GetValue();
                Collider.GetComponent<EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));
            }
        }
    }
}