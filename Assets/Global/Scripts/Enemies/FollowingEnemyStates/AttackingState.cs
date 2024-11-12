using UnityEditor.Search;
using UnityEngine;

namespace FollowEnemyStates
{
    public class AttackingState : StateBase
    {
        public AttackingState(FollowEnemy followEnemy) : base(followEnemy)
        {
        }
        public override void Enter()
        {
            Attack();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            followEnemy.attackTimeElapsed += Time.deltaTime;
            if (followEnemy.attackTimeElapsed >= followEnemy.attackCooldown)
            {
                followEnemy.attackTimeElapsed = 0f;
                followEnemy.toggleCanAttack(true);
                followEnemy.ChangeState(followEnemy.states["Following"]);

            }
        }
        private void Attack()
        {
            if (followEnemy.canAttack)
            {
                followEnemy.target.root.GetComponent<Player>().OnHit(followEnemy.attackDamage); // TODO: replace this when using EventSystem
                followEnemy.toggleCanAttack(false);
            }
        }
    }
}