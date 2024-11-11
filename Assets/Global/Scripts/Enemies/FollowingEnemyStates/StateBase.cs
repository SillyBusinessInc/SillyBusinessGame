namespace FollowEnemyStates
{
    public abstract class StateBase
    {
        protected FollowEnemy followEnemy;
        protected StateBase(FollowEnemy followEnemy)
        {
            this.followEnemy = followEnemy;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}