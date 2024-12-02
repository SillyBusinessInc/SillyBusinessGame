using UnityEngine;

namespace EnemiesNS
{
    public class MeleeDeadState : BaseDeadState
    {
        public MeleeDeadState(MeleeEnemy enemy) : base(enemy) { }

        public override void Enter()
        {
            base.Enter();
            enemy.animator.SetBool("No_anim", true);
        }

    }
}
