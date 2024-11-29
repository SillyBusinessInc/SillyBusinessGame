
using UnityEngine;
namespace EnemiesNS
{
    public class MeleeEnemy : EnemyBase
    {
        [Header("Melee Character Specific settings")]
        [Tooltip("Reference to the weapon's hitbox")]
        public Collider weapon;
        private bool playerHit = false;

        protected override void Start()
        {
            base.Start();
            Debug.Log(states.GetType().Name);
        }

        //
        // called in animations as events
        //
        public void EnableWeaponHitBox()
        {
            weapon.enabled = true;
        }

        public void DisableWeaponHitBox()
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