using System;
using UnityEngine;

public class FlipCollidor : MonoBehaviour
{
    public Player player;

    public void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.CompareTag("Enemy"))
        {
            if (player.Tail.flipDoDamage)
            {
                Debug.Log("flipcircle is doing damage");
                float actualDamage =
                    player.Tail.tailDoDamage * player.playerStatistic.AttackDamageMultiplier.GetValue();
                if (Collider.GetComponent<EnemyBase>() != null)
                {
                    Collider.GetComponent<EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));
                }
            }
        }
    }
}
