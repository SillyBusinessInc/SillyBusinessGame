
using UnityEngine;
namespace EnemiesNS
{
    public class MeleeEnemy : EnemyBase
    {
        [Header("Melee Character Specific settings")]
        [Tooltip("Reference to the weapon's hitbox")]

        private bool playerHit = false;

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
            playerHit = false;
        }

        public override void PlayerHit(PlayerObject playerObject, int damage)
        {
            if (playerHit) return;
            playerHit = true;
            base.PlayerHit(playerObject, damage);
        }

        protected override void SetupStateMachine()
        {
            states = new MeleeStates(this);
            ChangeState(states.Idle);
        }

    }
}