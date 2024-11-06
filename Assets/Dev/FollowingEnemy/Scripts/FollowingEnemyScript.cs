using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemyScript : EnemyScript
{
    // Finite state machine states
    private enum State
    {
        Roaming,
        Following
    }

    private State currentState;
    private NavMeshAgent agent;

    // Roaming behavior
    public float roamingRadius = 10f;
    public float roamingAngleRange = 90f;
    private Vector3 roamingNextDestination;

    // Following behavior
    private Transform target;
    public float followRange = 10f;

    // Vision cone parameters
    public float visionAngle = 45f;
    public float visionRange = 10f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Roaming;
        SetNewRoamingPosition();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Roaming:
                Roaming();
                if (TryDetectPlayer())
                {
                    currentState = State.Following;
                }
                break;

            case State.Following:
                Following();
                if (target == null || !IsPlayerInSight())
                {
                    currentState = State.Roaming;
                    target = null;
                    SetNewRoamingPosition();
                }
                break;
        }
    }

    private void Roaming()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNewRoamingPosition();
        }
    }

    private void SetNewRoamingPosition()
    {
        float angle = Random.Range(-roamingAngleRange / 2, roamingAngleRange / 2);
        Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
        Vector3 randomDirection = direction * Random.Range(1f, roamingRadius);

        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamingRadius, NavMesh.AllAreas))
        {
            roamingNextDestination = hit.position;
            agent.SetDestination(roamingNextDestination);
        }
    }

    private void Following()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    // Detects if a player is within the cone of vision and sets it as the target
    private bool TryDetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, visionRange);
        foreach (var hit in hits)
        {
            PlayerScript player = hit.GetComponent<PlayerScript>();
            if (player != null)
            {
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

                if (angleToPlayer < visionAngle / 2)
                {
                    RaycastHit rayHit;
                    if (Physics.Raycast(transform.position, directionToPlayer, out rayHit, visionRange))
                    {
                        if (rayHit.collider.GetComponent<PlayerScript>() != null)
                        {
                            target = player.transform;
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    // Checks if the player is still in sight
    private bool IsPlayerInSight()
    {
        if (target == null) return false;

        Vector3 directionToPlayer = (target.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer < visionAngle / 2 && Vector3.Distance(transform.position, target.position) <= visionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionRange))
            {
                if (hit.collider.GetComponent<PlayerScript>() != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Visualize the cone of vision in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2, 0) * transform.forward * visionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2, 0) * transform.forward * visionRange;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
