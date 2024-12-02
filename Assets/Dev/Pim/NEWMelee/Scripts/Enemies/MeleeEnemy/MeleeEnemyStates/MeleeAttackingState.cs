using UnityEngine;

namespace EnemiesNS
{
    public class MeleeAttackingState : BaseAttackingState
    {
        public MeleeAttackingState(MeleeEnemy enemy) : base(enemy) { }

        public override void Enter()
        {
            enemy.animator.SetInteger("Idle_var", 1);
            enemy.animator.SetBool("Idle", true);
            base.Enter();
        }

        public override void Exit()
        {
            enemy.animator.SetBool("Idle", false);
            base.Exit();
        }

    }
}