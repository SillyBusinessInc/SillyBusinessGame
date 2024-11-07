using UnityEngine;
using UnityEngine.AI;

namespace FollowingEnemy
{

    public class BaseState
    {
        protected FollowingEnemyScript enemy;
        public BaseState(FollowingEnemyScript enemy)
        {
            this.enemy = enemy;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }


        protected void SetNewRoamingPosition()
        {
            float angle = Random.Range(-enemy.roamingAngleRange / 2, enemy.roamingAngleRange / 2);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * enemy.transform.forward;
            Vector3 randomDirection = direction * Random.Range(1f, enemy.roamingRadius);

            randomDirection += enemy.transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, enemy.roamingRadius, NavMesh.AllAreas))
            {
                enemy.agent.SetDestination(hit.position);
            }
        }

        // Checks if the player is still in sight
        protected bool IsPlayerInSight()
        {
            if (enemy.target == null) return false;

            Vector3 directionToPlayer = (enemy.target.position - enemy.transform.position).normalized;
            float angleToPlayer = Vector3.Angle(enemy.transform.forward, directionToPlayer);

            if (angleToPlayer < enemy.visionAngle / 2 && Vector3.Distance(enemy.transform.position, enemy.target.position) <= enemy.visionRange)
            {
                RaycastHit hit;
                if (Physics.Raycast(enemy.transform.position, directionToPlayer, out hit, enemy.visionRange))
                {
                    if (hit.collider.GetComponent<PlayerScript>() != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
