using FollowingEnemy;
using UnityEngine;

public class FollowingState : FollowingEnemy.BaseState
{
    public FollowingState(FollowEnemyBase followEnemy) : base(followEnemy) { }
    public override void Enter()
    {
        // TODO: Remove after review
        Debug.Log("ENTERING: Following");
    }

    public override void Exit()
    {
        // TODO: Remove after review
        Debug.Log("EXITING: Following");
    }

    public override void Update()
    {
        Following();
        if (followEnemy.target == null || !IsPlayerInSight())
        {
            followEnemy.target = null;
            followEnemy.ChangeState(followEnemy.states["Roaming"]);
        }
    }
    private void Following()
    {
        if (followEnemy.target != null)
        {
            followEnemy.agent.SetDestination(followEnemy.target.position);
        }
    }

    // Checks if the player is still in sight
    protected bool IsPlayerInSight()
    {
        if (followEnemy.target == null) return false;

        Vector3 directionToPlayer = (followEnemy.target.position - followEnemy.transform.position).normalized;
        float angleToPlayer = Vector3.Angle(followEnemy.transform.forward, directionToPlayer);

        if (angleToPlayer < followEnemy.visionAngle / 2 && Vector3.Distance(followEnemy.transform.position, followEnemy.target.position) <= followEnemy.visionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(followEnemy.transform.position, directionToPlayer, out hit, followEnemy.visionRange))
            {
                if (hit.collider.GetComponent<Player>() != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

