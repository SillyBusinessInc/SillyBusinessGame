using UnityEngine;
using UnityEngine.AI;

public class DestinationPicker : MonoBehaviour
{
    public NavMeshAgent agent;
    public float roamingRadius = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNewDestination();
        }
    }

    protected void SetNewDestination()
    {
        // Pick a random point within the roaming radius around the agent's current position
        Vector3 randomDirection = Random.insideUnitSphere * roamingRadius;
        randomDirection += transform.position;

        // Ensure the point is on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamingRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
