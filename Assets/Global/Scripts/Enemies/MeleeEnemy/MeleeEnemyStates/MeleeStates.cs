namespace EnemiesNS
{
    public class MeleeStates : BaseStates
    {
        public MeleeStates(MeleeEnemy enemy) : base(enemy) { }

        // Override the creation methods to return Melee-specific states
        protected override BaseIdleState CreateIdleState(MobileEnemyBase enemy) => new MeleeIdleState((MeleeEnemy)enemy);
        protected override BaseRoamingState CreateRoamingState(MobileEnemyBase enemy) => new MeleeRoamingState((MeleeEnemy)enemy);
        protected override BaseChasingState CreateChasingState(MobileEnemyBase enemy) => new MeleeChasingState((MeleeEnemy)enemy);
        protected override BaseAttackingState CreateAttackingState(MobileEnemyBase enemy) => new MeleeAttackingState((MeleeEnemy)enemy);
        protected override BaseDeadState CreateDeadState(MobileEnemyBase enemy) => new MeleeDeadState((MeleeEnemy)enemy);

        // New properties to provide type-specific access if needed
        public new MeleeIdleState Idle => (MeleeIdleState)base.Idle;
        public new MeleeRoamingState Roaming => (MeleeRoamingState)base.Roaming;
        public new MeleeChasingState Chasing => (MeleeChasingState)base.Chasing;
        public new MeleeAttackingState Attacking => (MeleeAttackingState)base.Attacking;
        public new MeleeDeadState Dead => (MeleeDeadState)base.Dead;
    }
}