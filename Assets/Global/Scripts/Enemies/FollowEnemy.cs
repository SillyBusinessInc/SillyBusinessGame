using System.Collections.Generic;
using FollowEnemyStates;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : EnemyBase
{
    public FollowingEnemyStates states;
    private FollowEnemyStates.StateBase currentState;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;

    [Header("Roaming behavior")]
    public float roamingRadius = 10f;
    public float roamingAngleRange = 90f;

    [Header("Following behavior")]
    public Collider playerObject; // The player object is what the enemy is following
    public Transform target;

    [Header("Vision cone")]
    public float visionAngle = 45f;
    public float visionRange = 10f;

    [Header("Memory")]
    public float memoryDuration = 2f; // Time to "remember" the player's last position
    [HideInInspector] public float lastSeenTime = 0f;
    [HideInInspector] public Vector3 lastSeenLocation = Vector3.zero;


    [Header("Attacking behavior")]
    public float attackDamage = 1f; // hp damage
    public float attackRange = 5f;
    public float attackCooldown = 2f;
    public bool canAttack = true;
    public float attackTimeElapsed = 0f;

    [Header("Debugging")]
    [SerializeField] private string currentStateName = "none";

    private new void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        try
        {
            playerObject = GlobalReference.GetReference<PlayerReference>().PlayerObj.GetComponent<Collider>();
        }
        catch { }
        states = new FollowingEnemyStates(this);
        ChangeState(states.Roaming);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        };
    }

    public void ChangeState(FollowEnemyStates.StateBase state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
        currentStateName = state.GetType().Name;
    }

    public void toggleCanAttack(bool v)
    {
        canAttack = v;
        if (canAttack) attackTimeElapsed = 0f;
    }

    // Visualize the cone of vision in the editor
    private void OnDrawGizmos()
    {

        // Draw vision range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        // Draw vision cone
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2, 0) * transform.forward * visionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2, 0) * transform.forward * visionRange;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);


        // Draw the attack range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Debug
    [ContextMenu("DIE")]
    public void Die()
    {
        OnHit(100);
    }
}

