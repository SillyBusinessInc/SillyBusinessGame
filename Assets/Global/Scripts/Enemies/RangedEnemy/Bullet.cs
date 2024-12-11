using UnityEngine;

namespace EnemiesNS
{


    public class Bullet : MonoBehaviour
    {
        public float bulletSpeed = 10f;
        public float bulletLifeTime = 2f;
        public int bulletDamage = 1;
        public int bulletEnemyDamage = 5;
        public Vector3 bulletDirection;
        [Tooltip("The amount of knockback this enemy's attacks will apply")]
        [SerializeField]
        [Range(0f, 100f)]
        public float attackKnockback = 2f;
        public Bullet(Vector3 position)
        {
            bulletDirection = position;
        }
        void Start()
        {
            Shot();
        }

        

        void OnTriggerEnter(Collider hit)
        {   
            if (hit.gameObject.layer == 7 || hit.gameObject.layer == 9){
                PlayerHit(bulletDamage);
            }else if (hit.gameObject.tag == "Enemy"){
                //damage enemy
                hit.gameObject.GetComponent<EnemyBase>().OnHit(bulletEnemyDamage);
            }if(hit.gameObject.layer == 1 || hit.gameObject.layer == 2 || hit.gameObject.layer == 5 || hit.gameObject.layer == 10){
                return;
            }
            Destroy(gameObject);
        }

        private void PlayerHit(int damage)
        {
            Player player = GlobalReference.GetReference<PlayerReference>().Player;
            if (!player) return;
            player.OnHit(damage);
            // player.applyKnockback(CalculatedKnockback(playerObject), attackKnockback);
        }

        private Vector3 CalculatedKnockback(PlayerObject playerObject)
        {
            Vector3 directionToPlayer = playerObject.transform.position - transform.position;
            directionToPlayer.y = 0;
            directionToPlayer.Normalize();

            return directionToPlayer * attackKnockback;
        }

        void Shot()
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.linearVelocity = bulletDirection * bulletSpeed;
            

            // Destroy the bullet after its lifetime
            Destroy(gameObject, bulletLifeTime);
        }

    }
}