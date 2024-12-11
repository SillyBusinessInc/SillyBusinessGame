using UnityEngine;
using UnityEngine.AI;

namespace EnemiesNS
{
    public class BaseRoamingState : StateBase
    {
        public BaseRoamingState(EnemyBase enemy) : base(enemy) {}

        public override void Enter()
        {
            base.Enter();
            enemy.agent.speed = enemy.roamingSpeed;
            enemy.agent.acceleration = enemy.roamingAcceleration;
            // new Destination
            enemy.roamDestination = GetDestination();
            enemy.agent.SetDestination(enemy.roamDestination);
        }

        public override void Update() => base.Update();

        private Vector3 GetDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere * enemy.roamRange;
            randomDirection += enemy.spawnPos;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDirection, out hit, enemy.roamRange, NavMesh.AllAreas))
            {
                return hit.position;
            }
            return enemy.spawnPos;
        }
    }
}
