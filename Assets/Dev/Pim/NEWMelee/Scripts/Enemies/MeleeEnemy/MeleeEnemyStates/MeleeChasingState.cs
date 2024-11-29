using UnityEngine;

namespace EnemiesNS
{
    public class MeleeChasingState : BaseChasingState
    {
        public MeleeChasingState(MeleeEnemy enemy) : base(enemy) { }

        public override void Enter()
        {
            Debug.Log("triggering walk bool");
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
