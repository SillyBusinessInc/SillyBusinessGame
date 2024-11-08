namespace FollowingEnemy
{
    public abstract class StateBase
    {
        protected FollowEnemyBase followEnemy;
        protected StateBase(FollowEnemyBase followEnemy)
        {
            this.followEnemy = followEnemy;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}