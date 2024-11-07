using UnityEngine;
using UnityEngine.AI;
using FollowingEnemy;

public class RoamingState : BaseState
{
    public RoamingState(FollowingEnemyScript enemy) : base(enemy) { }

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
            enemy.ChangeState(enemy.states["Following"]);
        }
    }

    private void Roaming()
    {
        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.5f)
        {
            SetNewDestination();
        }
    }

    protected void SetNewDestination()
    {
        float angle = Random.Range(-enemy.roamingAngleRange / 2, enemy.roamingAngleRange / 2);
        Vector3 direction = Quaternion.Euler(0, angle, 0) * enemy.transform.forward;
        Vector3 randomDirection = direction * Random.Range(1f, enemy.roamingRadius);

        randomDirection += enemy.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, enemy.roamingRadius, NavMesh.AllAreas))
        {
            enemy.agent.SetDestination(hit.position);
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
