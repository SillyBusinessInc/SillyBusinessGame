using UnityEngine;
namespace EnemiesNS
{
    public class BaseAttackingState : StateBase
    {
        public BaseAttackingState(EnemyBase enemy) : base(enemy) { }

        public override void Enter()
        {
            enemy.agent.isStopped = true;
        }

        public override void Exit()
        {
            enemy.agent.isStopped = false;
        }

        public override void Update()
        {
            if (enemy.target == null) enemy.ChangeState(enemy.states.Roaming);

            // If the player is in range, attempt to face them
            if (IsWithinAttackRange())
            {
                FacePlayer();

                if (IsFacingPlayer()) Attack();
            }
            base.Update();
        }

        private void Attack()
        {
            if (!enemy.canAttack) return;

            // Proceed with the attack if the player exists and can be damaged
            if (enemy.target != null)
            {
                var player = enemy.target.root.GetComponent<Player>();
                if (player != null)
                {
                    enemy.animator.SetTrigger("TriggerAttackAnimation");
                    player.OnHit(enemy.attackDamage);
                }
            }

            // After attacking, disable attacking until cooldown is over
            enemy.toggleCanAttack(false);
        }

        private void FacePlayer()
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

        private bool IsFacingPlayer()
        {
            if (enemy.target == null) return false;

            // Check if the enemy is facing the player (within a certain threshold angle)
            Vector3 directionToPlayer = (enemy.target.position - enemy.transform.position).normalized;
            float angleToPlayer = Vector3.Angle(enemy.transform.forward, directionToPlayer);

            return angleToPlayer < 5f;
        }
    }
}