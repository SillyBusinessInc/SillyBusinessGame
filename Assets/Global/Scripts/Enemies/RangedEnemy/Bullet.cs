using UnityEngine;

namespace EnemiesNS
{


    public class Bullet : MonoBehaviour
    {
        public float bulletSpeed = 10f;
        public float bulletLifeTime = 2f;
        public int bulletDamage = 1;
        public Vector3 bulletDirection;
        [Tooltip("The amount of knockback this enemy's attacks will apply")]
        [SerializeField]
        [Range(0f, 100f)]
        public float attackKnockback = 10f;
        public Bullet(Vector3 position)
        {
            bulletDirection = position;
        }
        void Start()
        {
            Debug.Log("Shooting spawned");
            // enemy = this.GetComponentInParent<EnemyBase>();
            shot();
        }

        

        void OnTriggerEnter(Collider hit)
        {
            hit.TryGetComponent(out PlayerObject player);
            if (!player) return;
            PlayerHit(player, bulletDamage);
            Destroy(gameObject);
        }

        private void PlayerHit(PlayerObject playerObject, int damage)
        {
            Player player = playerObject.GetComponentInParent<Player>();
            if (!player) return;
            player.OnHit(damage);
            player.applyKnockback(CalculatedKnockback(playerObject), attackKnockback);
        }

        private Vector3 CalculatedKnockback(PlayerObject playerObject)
        {
            Vector3 directionToPlayer = playerObject.transform.position - transform.position;
            directionToPlayer.y = 0;
            directionToPlayer.Normalize();

            return directionToPlayer * attackKnockback;
        }

        void shot(){
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = bulletDirection * bulletSpeed;
            Destroy(gameObject, bulletLifeTime);
        }
    }
}