using UnityEngine;
namespace EnemiesNS
{


    public class RangedWeapon : MonoBehaviour
    {
        public float bulletSpeed = 10f;
        public float bulletLifeTime = 2f;
        public float bulletDamage = 10f;
        EnemyBase enemy;
        void Start()
        {
            enemy = this.GetComponentInParent<EnemyBase>();
            shot();
        }

        void OnTriggerEnter(Collider hit)
        {
            if (!enemy) return;
            hit.TryGetComponent(out PlayerObject player);
            if (!player) return;
            enemy.PlayerHit(player, enemy.attackDamage);
            Destroy(gameObject);
        }

        void shot(){
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = enemy.transform.forward * bulletSpeed;
            Destroy(gameObject, bulletLifeTime);
        }
    }
}