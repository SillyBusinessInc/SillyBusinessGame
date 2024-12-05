using UnityEngine;

namespace EnemiesNS
{
    public class RangeDeadState : BaseDeadState
    {
        public RangeDeadState(RangedEnemy enemy) : base(enemy) { }

        public override void Enter()
        {
            base.Enter();
            enemy.animator.SetInteger("DeathReason", 1);
            enemy.animator.SetTrigger("PlayDeath");

        }

    }
}
