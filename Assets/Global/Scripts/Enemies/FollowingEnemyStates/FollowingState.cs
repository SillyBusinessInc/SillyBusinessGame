using UnityEngine;

namespace FollowEnemyStates
{
    public class FollowingState : StateBase
    {
        public FollowingState(FollowEnemy followEnemy) : base(followEnemy)
        {
        }
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            Following();
            if (followEnemy.target == null || !IsPlayerInSight())
            {
                followEnemy.target = null;
                followEnemy.ChangeState(followEnemy.states["Roaming"]);
            }
            if (followEnemy.target && IsPlayerInSight() && IsPlayerInAttackRange())
            {
                followEnemy.ChangeState(followEnemy.states["Attacking"]);
            }
        }
        private void Following()
        {
            if (followEnemy.target != null)
            {
                followEnemy.agent.SetDestination(followEnemy.target.position);
            }
        }

        // Check if the player is still in sight
        protected bool IsPlayerInSight()
        {
            if (followEnemy.target == null) return false;

            Vector3 directionToPlayer = (followEnemy.target.position - followEnemy.transform.position).normalized;
            float angleToPlayer = Vector3.Angle(followEnemy.transform.forward, directionToPlayer);

            if (angleToPlayer < followEnemy.visionAngle / 2 && Vector3.Distance(followEnemy.transform.position, followEnemy.target.position) <= followEnemy.visionRange)
            {
                if (Physics.Raycast(followEnemy.transform.position,
                        directionToPlayer, out var hit,
                        followEnemy.visionRange))
                {
                    if (hit.collider == followEnemy.playerObject)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Check if player is within attackrange
        protected bool IsPlayerInAttackRange()
        {
            if (followEnemy.target == null) return false;

            float distanceToTarget = Vector3.Distance(followEnemy.transform.position, followEnemy.target.position);
            return distanceToTarget <= followEnemy.attackRange;
        }
    }
}