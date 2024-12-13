using UnityEngine;

namespace EnemiesNS
{
    public class MeleeWeapon : MonoBehaviour
    {
        MobileEnemyBase enemy;
        void Start()
        {
            enemy = this.GetComponentInParent<MobileEnemyBase>();
        }

        void OnTriggerEnter(Collider hit)
        {
            if (!enemy) return;
            hit.TryGetComponent(out PlayerObject player);
            if (!player) return;
            enemy.PlayerHit(player, enemy.attackDamage);
        }
    }
}