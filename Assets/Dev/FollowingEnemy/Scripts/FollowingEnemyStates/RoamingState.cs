using UnityEngine;
using FollowingEnemy;

public class RoamingState : BaseState
{
    public RoamingState(FollowingEnemyScript enemy) : base(enemy) { }

    public override void Enter()
    {
        Debug.Log("ENTERING: Roaming");
    }

    public override void Exit()
    {
        Debug.Log("EXITING: Roaming");
    }

    public override void Update()
    {
        Roaming();
        if (TryDetectPlayer())
        {
            enemy.ChangeState(enemy.states["Following"]);
        }
    }

    private void Roaming()
    {
        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.5f)
        {
            SetNewRoamingPosition();
        }
    }

    // Detects if a player is within cone of vision and set it as target
    private bool TryDetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(enemy.transform.position, enemy.visionRange);
        foreach (var hit in hits)
        {
            PlayerScript player = hit.GetComponent<PlayerScript>();
            if (player != null)
            {
                Vector3 directionToPlayer = (player.transform.position - enemy.transform.position).normalized;
                float angleToPlayer = Vector3.Angle(enemy.transform.forward, directionToPlayer);

                if (angleToPlayer < enemy.visionAngle / 2)
                {
                    RaycastHit rayHit;
                    if (Physics.Raycast(enemy.transform.position, directionToPlayer, out rayHit, enemy.visionRange))
                    {
                        if (rayHit.collider.GetComponent<PlayerScript>() != null)
                        {
                            enemy.target = player.transform;
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
