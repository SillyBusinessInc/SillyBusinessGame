using UnityEngine;

namespace EnemiesNS
{
    public class MeleeAttackingState : BaseAttackingState
    {
        public MeleeAttackingState(MeleeEnemy enemy) : base(enemy) { }

        public override void Enter()
        {
            enemy.EnableWeaponHitBox();
            enemy.animator.SetInteger("Attack_var", 0);
            enemy.animator.SetBool("Attack", true);
            base.Enter();
        }

        public override void Exit()
        {
            enemy.DisableWeaponHitBox();
            enemy.animator.SetBool("Attack", false);
            base.Exit();
        }

        protected override void Attack()
        {
            enemy.animator.SetTrigger("PlayAttack");
            base.Attack();
        }

    }
}