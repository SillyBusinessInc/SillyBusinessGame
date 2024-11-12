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
            followEnemy.ChangeState(followEnemy.states["Following"]);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
        private void Attack()
        {
            if (followEnemy.canAttack)
            {
                followEnemy.target.root.GetComponent<Player>().OnHit(followEnemy.attackDamage); // TODO: replace this when using EventSystem
                followEnemy.toggleCanAttack();
            }
        }
    }
}