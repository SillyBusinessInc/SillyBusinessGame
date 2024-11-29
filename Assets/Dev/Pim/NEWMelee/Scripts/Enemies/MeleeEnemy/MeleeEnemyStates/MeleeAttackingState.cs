using UnityEngine;

namespace EnemiesNS
{
    public class MeleeAttackingState : BaseAttackingState
    {
        public MeleeAttackingState(MeleeEnemy enemy) : base(enemy) { }

        public override void Enter()
        {
            Debug.Log("Triggering attack bool");
            enemy.animator.SetBool("Idle", true);
            enemy.animator.SetInteger("Idle_var", 1);
            base.Enter();
        }

        public override void Exit()
        {
            enemy.animator.SetBool("Idle", false);
            base.Exit();
        }

    }
}