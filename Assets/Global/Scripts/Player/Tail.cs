using System;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public Player player;

    public void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.CompareTag("Enemy"))
        {
            if(player.tailCanDoDamage)
            {
                float actualDamage = player.tailDoDamage;
                // calculate percentage, for example if damage is +20% it will calculate actual damage as damage * 1.2
                float percentage = (100 + player.playerStatistic.AttackDamageMultiplier.GetValue()) / 100;
                actualDamage *= percentage;
                Collider.GetComponent<EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));
            }
        }
    }
}