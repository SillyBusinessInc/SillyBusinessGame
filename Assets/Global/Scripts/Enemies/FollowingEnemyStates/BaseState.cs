namespace FollowingEnemy
{

    public class BaseState
    {
        protected FollowingEnemyScript enemy;
        public BaseState(FollowingEnemyScript enemy)
        {
            this.enemy = enemy;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}