namespace EnemiesNS
{
    public class RangeStates : BaseStates
    {
        public RangeStates(RangedEnemy enemy) : base(enemy) { }

        // Override the creation methods to return Melee-specific states
        protected override BaseIdleState CreateIdleState(EnemyBase enemy) => new RangeIdleState((RangedEnemy)enemy);
        protected override BaseRoamingState CreateRoamingState(EnemyBase enemy) => new RangeRoamingState((RangedEnemy)enemy);
        protected override BaseChasingState CreateChasingState(EnemyBase enemy) => new RangeChasingState((RangedEnemy)enemy);
        protected override BaseAttackingState CreateAttackingState(EnemyBase enemy) => new RangeAttackingState((RangedEnemy)enemy);
        protected override BaseDeadState CreateDeadState(EnemyBase enemy) => new RangeDeadState((RangedEnemy)enemy);

        // New properties to provide type-specific access if needed
        public new RangeIdleState Idle => (RangeIdleState)base.Idle;
        public new RangeRoamingState Roaming => (RangeRoamingState)base.Roaming;
        public new RangeChasingState Chasing => (RangeChasingState)base.Chasing;
        public new RangeAttackingState Attacking => (RangeAttackingState)base.Attacking;
        public new RangeDeadState Dead => (RangeDeadState)base.Dead;
    }
}