using UnityEngine;

namespace EnemiesNS
{
    public class MeleeDeadState : BaseDeadState
    {
        public MeleeDeadState(MeleeEnemy enemy) : base(enemy) { }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Triggering no anim bool");
            enemy.animator.SetBool("No_anim 0", true);
        }

    }
}
