using UnityEngine;

public class Slam : MonoBehaviour
{
    public Player player;

    public void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.CompareTag("Enemy"))
        {
            if(player.slamCanDoDamage)
            {
                Collider.GetComponent<EnemyBase>().OnHit(player.slamDamage);
            }
        }
    }
}