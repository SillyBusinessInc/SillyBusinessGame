using UnityEngine;

namespace EnemiesNS
{
    public class BaseDeadState : StateBase
    {
        public BaseDeadState(MobileEnemyBase enemy) : base(enemy) { }

        public override void Update()
        {
            return;
        }
    }
}