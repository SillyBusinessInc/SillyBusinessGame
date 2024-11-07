using FollowingEnemy;
using UnityEngine;

public class FollowingState : FollowingEnemy.BaseState
{
    public FollowingState(FollowingEnemyScript enemy) : base(enemy) { }
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
        if (enemy.target == null || !IsPlayerInSight())
        {
            enemy.target = null;
            enemy.ChangeState(enemy.states["Roaming"]);
        }
    }
    private void Following()
    {
        if (enemy.target != null)
        {
            enemy.agent.SetDestination(enemy.target.position);
        }
    }

    // Checks if the player is still in sight
    protected bool IsPlayerInSight()
    {
        if (enemy.target == null) return false;

        Vector3 directionToPlayer = (enemy.target.position - enemy.transform.position).normalized;
        float angleToPlayer = Vector3.Angle(enemy.transform.forward, directionToPlayer);

        if (angleToPlayer < enemy.visionAngle / 2 && Vector3.Distance(enemy.transform.position, enemy.target.position) <= enemy.visionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(enemy.transform.position, directionToPlayer, out hit, enemy.visionRange))
            {
                if (hit.collider.GetComponent<PlayerScript>() != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

