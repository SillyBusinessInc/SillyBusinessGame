using UnityEngine;

namespace FollowEnemyStates
{
    public abstract class StateBase
    {
        protected FollowEnemy followEnemy;
        
        protected StateBase(FollowEnemy followEnemy)
        {
            this.followEnemy = followEnemy;
        }

        public virtual void Enter() {}
        public virtual void Exit() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {}

        // Check if the player is in sight
        protected virtual bool IsPlayerInSight()
        {
            if (followEnemy.playerObject == null) return false;

            Collider[] hits = Physics.OverlapSphere(followEnemy.transform.position, followEnemy.visionRange);

            foreach (var hit in hits)
            {
                // Skip if it's not the player
                if (hit != followEnemy.playerObject) continue;

                // Check if the player is within the vision cone
                Vector3 directionToPlayer = (hit.transform.position - followEnemy.transform.position).normalized;
                float angleToPlayer = Vector3.Angle(followEnemy.transform.forward, directionToPlayer);
                if (angleToPlayer > followEnemy.visionAngle / 2) continue; // Player outside the vision cone

                // Raycast to check if line of sight is clear
                int layerMask = ~LayerMask.GetMask("Ignore Raycast");
                if (Physics.Raycast(followEnemy.transform.position, directionToPlayer, out RaycastHit rayHit, followEnemy.visionRange, layerMask))
                {
                    if (rayHit.collider == followEnemy.playerObject)
                    {
                        // Successfully detected player
                        followEnemy.target = rayHit.transform;
                        followEnemy.lastSeenTime = Time.time;
                        followEnemy.lastSeenLocation = hit.transform.position;
                        return true;
                    }
                }
            }

            return false;
        }

        // Check if player is within attackrange
        protected virtual bool IsPlayerInAttackRange()
        {
            if (followEnemy.target == null) return false;

            float distanceToTarget = Vector3.Distance(followEnemy.transform.position, followEnemy.target.position);
            return distanceToTarget <= followEnemy.attackRange;
        }
    }
}
