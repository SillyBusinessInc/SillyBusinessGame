using UnityEngine;

namespace EnemiesNS
{
    public abstract class StateBase
    {
        protected EnemyBase enemy;
        protected StateBase(EnemyBase enemy)
        {
            this.enemy = enemy;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }

        // Add methods here that need to be accessed by multiple different states
    }
}