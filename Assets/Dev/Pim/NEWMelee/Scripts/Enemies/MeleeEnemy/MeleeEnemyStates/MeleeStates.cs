namespace EnemiesNS
{
    public class MeleeStates : BaseStates
    {

        public new readonly MeleeIdleState Idle;
        public new readonly MeleeRoamingState Roaming;
        public new readonly MeleeChasingState Chasing;
        public new readonly MeleeAttackingState Attacking;
        public new readonly MeleeDeadState Dead;


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
