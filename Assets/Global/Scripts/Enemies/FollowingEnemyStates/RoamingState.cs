using UnityEngine;
using UnityEngine.AI;
using FollowingEnemy;

public class RoamingState : FollowingEnemy.BaseState
{
    public RoamingState(FollowEnemyBase followEnemy) : base(followEnemy) { }

    public override void Enter()
    {
        SetNewDestination();

        // TODO: Remove after review
        Debug.Log("ENTERING: Roaming");
    }

    public override void Exit()
    {
        // TODO: Remove after review
        Debug.Log("EXITING: Roaming");
    }

    public override void Update()
    {
        Roaming();
        if (TryDetectPlayer())
        {
            followEnemy.ChangeState(followEnemy.states["Following"]);
        }
    }

    private void Roaming()
    {
        if (!followEnemy.agent.pathPending && followEnemy.agent.remainingDistance < 0.5f)
        {
            SetNewDestination();
        }
    }

    protected void SetNewDestination()
    {
        float angle = Random.Range(-followEnemy.roamingAngleRange / 2, followEnemy.roamingAngleRange / 2);
        Vector3 direction = Quaternion.Euler(0, angle, 0) * followEnemy.transform.forward;
        Vector3 randomDirection = direction * Random.Range(1f, followEnemy.roamingRadius);

        randomDirection += followEnemy.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, followEnemy.roamingRadius, NavMesh.AllAreas))
        {
            followEnemy.agent.SetDestination(hit.position);
        }
    }

    // Detects if a player is within cone of vision and set it as target
    private bool TryDetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(followEnemy.transform.position, followEnemy.visionRange);
        foreach (var hit in hits)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
            {
                Vector3 directionToPlayer = (player.transform.position - followEnemy.transform.position).normalized;
                float angleToPlayer = Vector3.Angle(followEnemy.transform.forward, directionToPlayer);

                if (angleToPlayer < followEnemy.visionAngle / 2)
                {
                    RaycastHit rayHit;
                    if (Physics.Raycast(followEnemy.transform.position, directionToPlayer, out rayHit, followEnemy.visionRange))
                    {
                        if (rayHit.collider.GetComponent<Player>() != null)
                        {
                            followEnemy.target = player.transform;
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
