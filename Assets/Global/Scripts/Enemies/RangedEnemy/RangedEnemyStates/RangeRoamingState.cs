using UnityEngine;

namespace EnemiesNS
{
    public class RangeRoamingState : BaseRoamingState
    {
        public RangeRoamingState(RangedEnemy enemy) : base(enemy) { }

        public override void Enter()
        {
            enemy.animator.SetBool("Walk", true);
            base.Enter();
        }
        public override void Exit()
        {
            enemy.animator.SetBool("Walk", false);
            base.Exit();
        }

    }
}
