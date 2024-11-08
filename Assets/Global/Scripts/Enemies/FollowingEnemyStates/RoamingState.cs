using UnityEngine;
using UnityEngine.AI;


namespace FollowingEnemy
{
    public class RoamingState : FollowingEnemy.StateBase
    {
        public RoamingState(FollowEnemyBase followEnemy) : base(followEnemy) { }

        public override void Enter()
        {
            SetNewDestination();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            Roaming();
            if (TryDetectPlayer())
            {
                followEnemy.ChangeState(followEnemy.states["Following"]);
            }
        }

        private void Roaming()
        {
            if (!followEnemy.agent.pathPending && followEnemy.agent.remainingDistance < 0.5f)
            {
                SetNewDestination();
            }
        }

        protected void SetNewDestination()
        {
            float angle = Random.Range(-followEnemy.roamingAngleRange / 2, followEnemy.roamingAngleRange / 2);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * followEnemy.transform.forward;
            Vector3 randomDirection = direction * Random.Range(1f, followEnemy.roamingRadius);

            randomDirection += followEnemy.transform.position;
            if (NavMesh.SamplePosition(randomDirection, out var hit, 
                    followEnemy.roamingRadius, NavMesh.AllAreas))
            {
                followEnemy.agent.SetDestination(hit.position);
            }
        }

        // Detects if a player is within cone of vision and set it as target
        private bool TryDetectPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(followEnemy.transform.position, followEnemy.visionRange);
            foreach (var hit in hits)
            {
                if (hit == followEnemy.playerObject)
                {
                    Vector3 directionToPlayer = (followEnemy.playerObject.transform.position - 
                                                 followEnemy.transform.position).normalized;
                    float angleToPlayer = Vector3.Angle(followEnemy.transform.forward, directionToPlayer);
                    
                    if (angleToPlayer < followEnemy.visionAngle / 2)
                    {
                        if (Physics.Raycast(followEnemy.transform.position, 
                                directionToPlayer, out var rayHit, followEnemy.visionRange))
                        {
                            if (rayHit.collider == followEnemy.playerObject)
                            {
                                followEnemy.target = followEnemy.playerObject.transform;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}