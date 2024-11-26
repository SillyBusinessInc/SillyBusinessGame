using UnityEngine;

namespace EnemiesNS
{
    public abstract class StateBase
    {
        protected EnemyBase enemy;
        protected StateBase(EnemyBase enemy)
        {
            this.enemy = enemy;
        }

        public virtual void Enter()
        {
            Debug.Log($"Entering state: {enemy.currentState.GetType().Name}");
        }
        public virtual void Exit() { }
        public virtual void Update()
        {
            CheckState();
        }
        public virtual void FixedUpdate() { }

        //
        // Add methods here that need to be accessed by multiple different states
        //

        protected void CheckState()
        {

            // dead or alive check
            if (enemy.currentState == enemy.states.Dead) return;

            // attack
            Debug.Log("Check to attack");
            if (enemy.currentState != enemy.states.Attacking && enemy.canAttack && IsWithinAttackRange())
            {
                enemy.ChangeState(enemy.states.Attacking);
                return;
            }
            Debug.Log("Check to chase");
            // chase
            if (enemy.currentState != enemy.states.Chasing)
            {
                if (!enemy.isChasing) enemy.ChangeState(enemy.states.Chasing);
                return;
            }
            Debug.Log("Check to roam");
            // roaming
            if (enemy.currentState != enemy.states.Roaming && !enemy.isIdling)
            {
                enemy.ChangeState(enemy.states.Roaming);
                return;
            }
            Debug.Log("Check to Idle");
            // idle           
            if (enemy.currentState != enemy.states.Idle && enemy.agent.remainingDistance < 1)
            {
                enemy.ChangeState(enemy.states.Idle);
                return;
            }
        }

        protected bool IsWithinChaseRange()
        {
            //early return for when already chasing
            if (enemy.isChasing) return true;
            float distance = Vector3.Distance(enemy.transform.position, enemy.target.position);
            Debug.Log(distance);
            return distance <= enemy.chaseRange;
        }

        protected bool IsWithinAttackRange()
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.target.position);
            Debug.Log(distance);
            return distance <= enemy.attackRange;
        }
    }
}