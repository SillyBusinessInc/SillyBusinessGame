namespace FollowEnemyStates
{
    public class FollowingEnemyStates
    {
        public readonly StateBase Roaming;
        public readonly StateBase Following;
        public readonly StateBase Attacking;


        public FollowingEnemyStates(FollowEnemy followEnemy)
        {
            Roaming = new RoamingState(followEnemy);
            Following = new FollowingState(followEnemy);
            Attacking = new AttackingState(followEnemy);
        }
    }
}