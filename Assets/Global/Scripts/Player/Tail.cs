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
                Collider.GetComponent<EnemyBase>().OnHit(player.tailDoDamage);
            }
        }
    }
}