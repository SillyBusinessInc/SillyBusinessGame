using FollowingEnemy;
using UnityEngine;

public class FollowingState : BaseState
{
    public FollowingState(FollowingEnemyScript enemy) : base(enemy) { }
    public override void Enter()
    {
        Debug.Log("ENTERING: Following");
    }

    public override void Exit()
    {
        Debug.Log("EXITING: Following");
    }

    public override void Update()
    {
        Following();
        if (enemy.target == null || !IsPlayerInSight())
        {
            enemy.ChangeState(enemy.states["Roaming"]);
            enemy.target = null;
            SetNewRoamingPosition();
        }
    }
    private void Following()
    {
        if (enemy.target != null)
        {
            enemy.agent.SetDestination(enemy.target.position);
        }
    }

}
