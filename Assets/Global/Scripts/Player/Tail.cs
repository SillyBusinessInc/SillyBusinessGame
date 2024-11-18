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
                player.playerStatistic.AttackDamageMultiplier.AddMultiplier("damage", player.tailDoDamage, false);
                Collider.GetComponent<EnemyBase>().OnHit(player.playerStatistic.AttackDamageMultiplier.GetValueInt());
                // Collider.GetComponent<EnemyBase>().OnHit(player.tailDoDamage);
            }
        }
    }
}