namespace EnemiesNS
{
    public class BaseStates
    {
        public readonly BaseIdleState Idle;
        public readonly BaseRoamingState Roaming;
        public readonly BaseChasingState Chasing;
        public readonly BaseAttackingState Attacking;
        public readonly BaseDeadState Dead;


        public BaseStates(EnemyBase enemy)
        {
            Idle = new BaseIdleState(enemy);
            Roaming = new BaseRoamingState(enemy);
            Chasing = new BaseChasingState(enemy);
            Attacking = new BaseAttackingState(enemy);
            Dead = new BaseDeadState(enemy);
        }
    }
}
