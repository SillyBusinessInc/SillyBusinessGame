using UnityEngine;

namespace EnemiesNS
{
    public class BaseIdleState : StateBase
    {
        public BaseIdleState(EnemyBase enemy) : base(enemy) { }

        public override void Enter()
        {
            base.Enter();
            enemy.isIdling = true;
            enemy.idleWaitTime = GetIdleWaitValue();
            enemy.idleWaitElapsed = 0;
            Debug.Log($"idling for: {enemy.idleWaitTime}", enemy);
            enemy.agent.isStopped = true;
        }

        public override void Exit()
        {
            base.Exit();
            enemy.agent.isStopped = false;
        }

        public override void Update()
        {
            enemy.idleWaitElapsed += Time.deltaTime;
            if (enemy.idleWaitElapsed >= enemy.idleWaitTime) enemy.isIdling = false;
            base.Update();

        }

        private float GetIdleWaitValue()
        {
            return Random.Range(enemy.idleTime * (1 - enemy.idleVariance), enemy.idleTime * (1 + enemy.idleVariance));
        }

    }
}
