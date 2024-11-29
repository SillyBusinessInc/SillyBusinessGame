namespace EnemiesNS
{
    public class MeleeStates : BaseStates
    {

        public new readonly BaseIdleState Idle;
        public new readonly BaseRoamingState Roaming;
        public new readonly BaseChasingState Chasing;
        public new readonly BaseAttackingState Attacking;
        public new readonly BaseDeadState Dead;


        public MeleeStates(MeleeEnemy enemy) : base(enemy)
        {
            Idle = new MeleeIdleState(enemy);
            Roaming = new MeleeRoamingState(enemy);
            Chasing = new MeleeChasingState(enemy);
            Attacking = new MeleeAttackingState(enemy);
            Dead = new MeleeDeadState(enemy);
        }

    }
}
