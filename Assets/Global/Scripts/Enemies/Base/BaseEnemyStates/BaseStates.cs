namespace EnemiesNS
{
    public class BaseStates
    {
        public readonly BaseIdleState Idle;
        public readonly BaseRoamingState Roaming;
        public readonly BaseChasingState Chasing;
        public readonly BaseAttackingState Attacking;
        public readonly BaseDeadState Dead;

        public BaseStates(MobileEnemyBase enemy)
        {
            Idle = CreateIdleState(enemy);
            Roaming = CreateRoamingState(enemy);
            Chasing = CreateChasingState(enemy);
            Attacking = CreateAttackingState(enemy);
            Dead = CreateDeadState(enemy);
        }

        // Virtual methods to allow overriding in derived classes
        protected virtual BaseIdleState CreateIdleState(MobileEnemyBase enemy) => new BaseIdleState(enemy);
        protected virtual BaseRoamingState CreateRoamingState(MobileEnemyBase enemy) => new BaseRoamingState(enemy);
        protected virtual BaseChasingState CreateChasingState(MobileEnemyBase enemy) => new BaseChasingState(enemy);
        protected virtual BaseAttackingState CreateAttackingState(MobileEnemyBase enemy) => new BaseAttackingState(enemy);
        protected virtual BaseDeadState CreateDeadState(MobileEnemyBase enemy) => new BaseDeadState(enemy);
    }
}