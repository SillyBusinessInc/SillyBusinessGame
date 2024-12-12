using System;
using UnityEngine;
public class FlipCollidor : MonoBehaviour
{
    public Player player;

    public void OnTriggerEnter(Collider Collider)
    {
        if (!Collider.gameObject.CompareTag("Enemy") ||
            !player.Tail.flipCanDoDamage ||
            Collider.GetComponent<EnemiesNS.EnemyBase>() == null
        ) return;

        float actualDamage = player.Tail.tailDoDamage * player.playerStatistic.AttackDamageMultiplier.GetValue();
        Collider.GetComponent<EnemiesNS.EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));

        // enemy 
        EnemiesNS.EnemyBase enemy = Collider.GetComponent<EnemiesNS.EnemyBase>();
        Debug.Log("Enemy: " + enemy);
        if (enemy != null)
        {

            Vector3 kb = -enemy.transform.forward * 500;

            enemy.DoKnockback(kb, 1.5f);


        }

    }
}
