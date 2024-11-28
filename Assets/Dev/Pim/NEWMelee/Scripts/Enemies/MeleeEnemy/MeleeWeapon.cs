using UnityEngine;
namespace EnemiesNS
{
    public class MeleeWeapon : MonoBehaviour
    {
        EnemyBase enemy;
        void Start()
        {
            enemy = this.GetComponentInParent<EnemyBase>();
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
