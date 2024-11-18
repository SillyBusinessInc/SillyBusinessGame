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
                float actualDamage = player.tailDoDamage * player.playerStatistic.AttackDamageMultiplier.GetValue();
                Collider.GetComponent<EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));
            }
        }
    }
}