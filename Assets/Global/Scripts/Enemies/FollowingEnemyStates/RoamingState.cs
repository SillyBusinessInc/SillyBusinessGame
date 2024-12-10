using UnityEngine;
using UnityEngine.AI;

namespace FollowEnemyStates
{
    public class RoamingState : StateBase
    {
        public RoamingState(FollowEnemy followEnemy) : base(followEnemy) {}

        public override void Enter()
        {
            Debug.Log("Entering Roaming State.");
            followEnemy.lastSeenLocation = Vector3.zero;
            followEnemy.target = null;
            SetNewDestination();
        }

        public override void Exit() {}

        public override void Update()
        {
            Roaming();
            if (IsPlayerInSight())
            {
                followEnemy.ChangeState(followEnemy.states.Following);
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
    }
}