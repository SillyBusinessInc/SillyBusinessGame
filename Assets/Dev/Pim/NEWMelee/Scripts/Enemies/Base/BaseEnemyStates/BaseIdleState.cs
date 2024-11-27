using UnityEngine;

namespace EnemiesNS
{
    public class BaseIdleState : StateBase
    {
        public BaseIdleState(EnemyBase enemy) : base(enemy) { }

        public override void Enter()
        {
            base.Enter();

            enemy.idleWaitTime = GetIdleWaitValue();
            enemy.toggleIsIdling(true);
            enemy.FreezeMovement(true);
        }

        public override void Exit()
        {

            base.Exit();
        }

        private float GetIdleWaitValue()
        {
            return Random.Range(enemy.idleTime * (1 - enemy.idleVariance), enemy.idleTime * (1 + enemy.idleVariance));
        }

    }
}
