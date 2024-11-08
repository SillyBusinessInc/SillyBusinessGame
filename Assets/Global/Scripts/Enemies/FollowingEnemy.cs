using System.Collections.Generic;
using FollowingEnemy;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemyBase : EnemyBase
{
    public Dictionary<string, FollowingEnemy.StateBase> states;
    private FollowingEnemy.StateBase currentState;
    
    [HideInInspector] public NavMeshAgent agent;

    [Header("Roaming behavior")]
    public float roamingRadius = 10f;
    public float roamingAngleRange = 90f;
    // public Vector3 roamingNextDestination; // never used?

    [Header("Following behavior")]
    public Collider playerObject; // The player object is what the enemy is following
    public Transform target;
    // public float followRange = 10f; // never used?
    
    [Header("Vision cone")]
    public float visionAngle = 45f;
    public float visionRange = 10f;
    
    [Header("Debugging")]
    [SerializeField] private string currentStateName = "none";
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        states = new Dictionary<string, FollowingEnemy.StateBase>
            {
                {"Roaming", new RoamingState(this)},
                {"Following", new FollowingState(this)}

        };
        ChangeState(states["Roaming"]);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        };
    }

    public void ChangeState(FollowingEnemy.StateBase state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
        currentStateName = state.GetType().Name;
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

