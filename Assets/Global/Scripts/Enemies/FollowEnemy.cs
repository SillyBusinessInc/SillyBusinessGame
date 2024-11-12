using System.Collections.Generic;
using FollowEnemyStates;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : EnemyBase
{
    public Dictionary<string, FollowEnemyStates.StateBase> states;
    private FollowEnemyStates.StateBase currentState;

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

    [Header("Attacking behavior")]
    public float attackDamage = 25f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public bool canAttack = true;
    private float attackTimeElapsed = 0f;

    [Header("Debugging")]
    [SerializeField] private string currentStateName = "none";

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        states = new Dictionary<string, FollowEnemyStates.StateBase>
            {
                {"Roaming", new RoamingState(this)},
                {"Following", new FollowingState(this)},
                {"Attacking", new FollowEnemyStates.AttackingState(this)}, // unfortunately i need to specify namespace here to combat namespace conflicts.

        };
        ChangeState(states["Roaming"]);
    }

    private void Update()
    {
        if (!canAttack)
        {
            attackTimeElapsed += Time.deltaTime;
            if (attackTimeElapsed >= attackCooldown)
            {
                toggleCanAttack();
            }
        }
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

    public void toggleCanAttack()
    {
        canAttack = !canAttack;
        if (canAttack) attackTimeElapsed = 0f;
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

