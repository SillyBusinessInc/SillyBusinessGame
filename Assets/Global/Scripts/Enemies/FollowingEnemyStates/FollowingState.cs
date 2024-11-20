using UnityEngine;

namespace FollowEnemyStates
{
    public class FollowingState : StateBase
    {
        public FollowingState(FollowEnemy followEnemy) : base(followEnemy)
        {
        }

        public override void Update()
        {
            Following();

            // Check for sight and attack range
            bool playerInSight = IsPlayerInSight();
            bool playerInAttackRange = IsPlayerInAttackRange();

            if (!playerInSight && Time.time - followEnemy.lastSeenTime > followEnemy.memoryDuration)
            {
                // No sight and memory expired, return to roaming
                followEnemy.ChangeState(followEnemy.states.Roaming);
            }
            else if (playerInAttackRange)
            {
                // In attack range, switch to attacking
                followEnemy.ChangeState(followEnemy.states.Attacking);
            }
        }

        private void Following()
        {
            if (followEnemy.target != null)
            {
                // Move towards the player's current position
                followEnemy.agent.SetDestination(followEnemy.target.position);
            }
            else if (Time.time - followEnemy.lastSeenTime <= followEnemy.memoryDuration)
            {
                // Move towards the last seen location
                followEnemy.agent.SetDestination(followEnemy.lastSeenLocation);
            }
        }
    }
}