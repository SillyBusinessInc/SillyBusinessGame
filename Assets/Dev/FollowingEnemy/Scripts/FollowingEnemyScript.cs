using System.Collections.Generic;
using FollowingEnemy;
using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemyScript : EnemyScript
{
    public Dictionary<string, FollowingEnemy.BaseState> states;

    public FollowingEnemy.BaseState currentState;
    public NavMeshAgent agent;

    // Roaming behavior
    public float roamingRadius = 10f;
    public float roamingAngleRange = 90f;
    public Vector3 roamingNextDestination;

    // Following behavior
    public Transform target;
    public float followRange = 10f;

    // Vision cone parameters
    public float visionAngle = 45f;
    public float visionRange = 10f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        states = new Dictionary<string, FollowingEnemy.BaseState>
            {
                {"Roaming", new RoamingState(this)},
                {"Following", new FollowingState(this)}

        };
        currentState = states["Roaming"];
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        };
    }

    public void ChangeState(FollowingEnemy.BaseState state)
    {
        if (currentState != null) currentState.Exit();
        currentState = state;
        if (currentState != null) currentState.Enter();
    }

    // Visualize the cone of vision in the editor
    // TODO: Remove for build
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

