using UnityEngine;

public class Slam : MonoBehaviour
{
    public Player player;

    public void OnTriggerEnter(Collider Collider)
    {
        player.isGrounded = true;
        if (Collider.gameObject.CompareTag("Enemy"))
        {
            if (player.slamCanDoDamage)
            {
                Collider.GetComponent<EnemyBase>().OnHit(player.slamDamage);
            }
        }
        if (player.isSlamming)
        {
            player.isSlamming = false;
            player.SetState(player.states.Idle);
        }
    }

    public void OnTriggerStay(Collider Collider)
    {
        player.isGrounded = true;
    }

    public void OnTriggerExit(Collider Collider)
    {
        player.isGrounded = false;
    }
}
