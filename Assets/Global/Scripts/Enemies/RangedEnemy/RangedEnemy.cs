
using UnityEngine;
namespace EnemiesNS
{
    public class RangedEnemy : EnemyBase
    {
        //TODO: this is a quick fix to get the demo out the door, make this nicer
        // [Header("Melee Character Specific settings")]
        // [Tooltip("Reference to the weapon's hitbox")]



        //
        // called in animations as events
        //
        public override void EnableWeaponHitBox()
        {
            weapon.enabled = true;
        }

        public override void DisableWeaponHitBox()
        {
            weapon.enabled = false;
        }

        public override void PlayerHit(PlayerObject playerObject, int damage)
        {
            if (playerHit) return;
            playerHit = true;
            base.PlayerHit(playerObject, damage);
        }

        protected override void SetupStateMachine()
        {
            states = new RangeStates(this);
            ChangeState(states.Idle);
        }

    }
}