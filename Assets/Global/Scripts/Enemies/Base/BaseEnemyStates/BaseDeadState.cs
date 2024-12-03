using System;
using System.Collections;
using UnityEngine;

namespace EnemiesNS
{
    public class BaseDeadState : StateBase
    {
        public BaseDeadState(EnemyBase enemy) : base(enemy) { }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("DEAD ENEMY", enemy);

        }

        public override void Update()
        {
            return;
        }


    }
}