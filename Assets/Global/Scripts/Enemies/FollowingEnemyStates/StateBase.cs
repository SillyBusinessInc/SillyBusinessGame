namespace FollowingEnemy
{

    public abstract class BaseState
    {
        protected FollowEnemyBase followEnemy;
        protected BaseState(FollowEnemyBase followEnemy)
        {
            this.followEnemy = followEnemy;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}