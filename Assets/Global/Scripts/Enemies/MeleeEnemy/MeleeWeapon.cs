using UnityEngine;

namespace EnemiesNS
{


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //TODO: SET COLLIDER SIZE BACK TO NORMAL AND DELETE WHEN ATTACK ANIMATIONS HAVE BEEN IMPLEMENTED               //
    //                                                                                                             //
    // ENLARGED THE COLLIDER FOR MELEE DUE TO MISSING ATTACK ANIMS                                                 //
    // ORIGINAL CENTER: Vector3 Vector3(0,-0.0656406805,1.38706756)                                                                                                         //
    // ORIGINAL SIZE: Vector3(0.193226293,0.370392919,2.06135321)                                                     //
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