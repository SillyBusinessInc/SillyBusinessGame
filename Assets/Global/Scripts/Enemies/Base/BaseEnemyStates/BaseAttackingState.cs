using UnityEngine;
namespace EnemiesNS
{
    public class BaseAttackingState : StateBase
    {
        public BaseAttackingState(EnemyBase enemy) : base(enemy) { }

        protected Player player;
        protected int attacksThisState;
        protected AnimatorStateInfo animatorStateInfo;

        public override void Enter()
        {
            base.Enter();
            enemy.FreezeMovement(true);
        }


        public override void Exit()
        {
            attacksThisState = 0;
            base.Exit();
        }

        public override void Update()
        {
            if (!enemy.animator.IsInTransition(0)) animatorStateInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);
            if (enemy.target == null) enemy.ChangeState(enemy.states.Idle);
            //TODO: this is a quick fix to get the demo out the door, make this nicer
            if (animatorStateInfo.normalizedTime / attacksThisState >= 1) enemy.toggleInAttackAnim(false, animatorStateInfo.normalizedTime / attacksThisState);
            if (animatorStateInfo.normalizedTime / attacksThisState >= 0.7)
            {
                enemy.DisableWeaponHitBox();
            }
            else if (animatorStateInfo.normalizedTime / attacksThisState >= 0.5)
            {
                if (!enemy.weapon.enabled)
                {
                    enemy.EnableWeaponHitBox();
                }
            }

            // If the player is in range, attempt to face them
            if (IsWithinAttackRange() && canAttack())
            {
                FacePlayer();
                if (IsFacingPlayer()) Attack();
            }

            // check if we can still attack, then early return so we dont run the base update and dont trigger attack cooldown.
            if (attacksThisState < enemy.attacksPerCooldown && IsWithinAttackRange()) return;

            enemy.toggleCanAttack(false);
            base.Update();
        }

        protected virtual void Attack()
        {

            enemy.inAttackAnim = true;
            // Proceed with the attack if the player exists and can be damaged
            attacksThisState++;
            enemy.toggleIsRecovering(true);
        }

        protected void FacePlayer()
        {
            if (enemy.target == null) return;

            // Get direction to the player
            Vector3 directionToPlayer = (enemy.target.position - enemy.transform.position).normalized;

            // Calculate the rotation required to face the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // Smoothly rotate towards the player
            enemy.transform.rotation = Quaternion.Slerp(
                enemy.transform.rotation,
                targetRotation,
                Time.deltaTime * enemy.agent.angularSpeed
            );
        }

        protected bool IsFacingPlayer()
        {
            if (enemy.target == null) return false;

            // Check if the enemy is facing the player (within a certain threshold angle)
            Vector3 directionToPlayer = (enemy.target.position - enemy.transform.position).normalized;
            float angleToPlayer = Vector3.Angle(enemy.transform.forward, directionToPlayer);

            return angleToPlayer < enemy.facingPlayerVarianceAngle;
        }

        protected bool canAttack()
        {
            bool canATK = true;
            if (!enemy.canAttack) canATK = false;
            if (enemy.isRecovering) canATK = false;
            if (enemy.target == null) canATK = false;
            if (enemy.inAttackAnim) canATK = false;
            return canATK;
        }
    }
}