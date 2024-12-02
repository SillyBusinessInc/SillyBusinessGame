using UnityEngine;
namespace EnemiesNS
{


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //TODO: SET COLLIDER SIZE BACK TO NORMAL AND DELETE WHEN ATTACK ANIMATIONS HAVE BEEN IMPLEMENTED               //
    //                                                                                                             //
    // ENLARGED THE COLLIDER FOR MELEE DUE TO MISSING ATTACK ANIMS                                                 //
    // ORIGINAL CENTER: Vector3 (0, -0.03822787, 0.4769854)                                                                                                            //
    // ORIGINAL SIZE: Vector3 (0.1932263, 0.3155826, 3.881517)                                                     //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////



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