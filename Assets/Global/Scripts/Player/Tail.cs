using System;
using UnityEngine;
using System.Collections.Generic;
public class Tail : MonoBehaviour
{
    public Player player;

    public void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.CompareTag("Enemy"))
        {
            if(player.collidersEnemy.Contains(Collider))
            {
                return;
            }
            player.collidersEnemy.Add(Collider);
            if(player.tailCanDoDamage)
            {
                float actualDamage = player.tailDoDamage * player.playerStatistic.AttackDamageMultiplier.GetValue();
                if (Collider.GetComponent<EnemyBase>() != null){
                    Collider.GetComponent<EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));
                }
            }
        }
    }
}