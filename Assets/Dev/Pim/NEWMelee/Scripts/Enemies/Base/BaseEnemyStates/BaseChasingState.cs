using UnityEngine;

namespace EnemiesNS
{
    public class BaseChasingState : StateBase
    {
        public BaseChasingState(EnemyBase enemy) : base(enemy) { }

        public override void Enter()
        {
            base.Enter();
            enemy.isChasing = true;
        }

        public override void Update()
        {
            enemy.agent.SetDestination(enemy.target.transform.position);

            base.Update();
        }
    }
}