using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float airBornMovementFactor = 0.5f;
    public float glideDrag = 2f;
    public float dodgeRollSpeed = 10f;
    public float dodgeRollDuration = 1f;

    public float degreesToRotate = 50.0f;

    [Header("Stats")]
    public PlayerStatistic playerStatistic = new();

    [Header("Attack")]
    public float attackResettingTime = 2f;
    public float TailTurnSpeed = 40f;
    public int slamDamage = 10;
    public int firstTailDamage = 10;
    public int secondTailDamage = 15;
    public float slamForce = 2.0f;
    public BoxCollider TransformTail;

    [Header("References")]
    [FormerlySerializedAs("playerRb")]
    public Rigidbody rb;
    public Transform orientation;

    [HideInInspector]
    public bool slamCanDoDamage = false;

    [HideInInspector]
    public int attackCounter;

    [HideInInspector]
    public int tailDoDamage;

    [HideInInspector]
    public bool isSlamming;

    [HideInInspector]
    public float activeAttackCooldown;

    [HideInInspector]
    public bool canDodgeRoll = true;

    [HideInInspector]
    public int currentJumps = 0;

    [HideInInspector]
    public float horizontalInput;

    [HideInInspector]
    public float verticalInput;

    [HideInInspector]
    public bool tailCanDoDamage = false;

    [HideInInspector]
    public PlayerStates states;
    public StateBase currentState;

    [HideInInspector]
    public Vector2 movementInput;

    [HideInInspector]

    public List<Collider> collidersEnemy;

    [Header("Debugging")]
    [SerializeField]
    private string currentStateName = "none";
    public bool isGrounded;

    // private PlayerInputActions inputActions;
    [HideInInspector]
    public float groundCheckDistance;

    void Start()
    {
        states = new PlayerStates(this);
        SetState(states.Idle);
        // health and maxHealth should be the same value at the start of game
        collidersEnemy = new List<Collider>();

        playerStatistic.Health = playerStatistic.MaxHealth.GetValue();
        GlobalReference.AttemptInvoke(Events.HEALTH_CHANGED);
    }

    void Update()
    {
        RaycastDown();
        currentState.Update();
        RotatePlayerObj();
        activeAttackCooldown =
            currentState.GetType().Name != "AttackingState"
                ? activeAttackCooldown + Time.deltaTime
                : 0.0f;
        if (activeAttackCooldown >= this.attackResettingTime)
        {
            attackCounter = 0;
            activeAttackCooldown = 0.0f;
        }
        if (isGrounded)
        {
            canDodgeRoll = true;
        }
    }

    void FixedUpdate() => currentState.FixedUpdate();

    public void OnCollisionEnter(Collision collision)
    {
        if (isSlamming)
        {
            isSlamming = false;
            SetState(states.Idle);
        }
        currentState.OnCollision(collision);
    }

    public void OnCollisionExit(Collision collision) { }

    private void RaycastDown()
    {
        groundCheckDistance = rb.GetComponent<Collider>().bounds.extents.y;
        Vector3[] raycastOffsets = new Vector3[]
        {
            Vector3.zero, 
            new Vector3(0, 0, rb.GetComponent<Collider>().bounds.extents.z), 
            new Vector3(0, 0, -rb.GetComponent<Collider>().bounds.extents.z),
            new Vector3(rb.GetComponent<Collider>().bounds.extents.x, 0, 0), 
            new Vector3(-rb.GetComponent<Collider>().bounds.extents.x,0,0) ,
        };

        foreach (Vector3 offset in raycastOffsets)
        {
            Vector3 raycastPosition = rb.position + offset;
            if (Physics.Raycast(raycastPosition,Vector3.down,out RaycastHit hit,groundCheckDistance))
            {
                if (!hit.collider.gameObject.CompareTag("Player"))
                {
                    if (Vector3.Angle(Vector3.up, hit.normal) < degreesToRotate)
                    {
                        currentJumps = 0;
                        isGrounded = true;
                        return;
                    }
                }
            }
        }
        isGrounded = false; 
    }

    public void SetState(StateBase newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        currentStateName = newState.GetType().Name;
    }

    public Vector3 GetDirection()
    {
        Vector3 moveDirection =
            orientation.forward * movementInput.y + orientation.right * movementInput.x;

        return moveDirection.normalized;
    }

    private void RotatePlayerObj()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            var direction = Vector3.ProjectOnPlane(rb.linearVelocity, Vector3.up).normalized;
            if (direction != Vector3.zero)
                rb.MoveRotation(Quaternion.LookRotation(direction));
        }
    }

    // If we go the event route this should change right?
    public void OnHit(float damage)
    {
        playerStatistic.Health -= damage;
        GlobalReference.AttemptInvoke(Events.HEALTH_CHANGED);

        if (playerStatistic.Health <= 0) OnDeath();

        Debug.Log(playerStatistic.Health);
        Debug.Log(GlobalReference.PlayerStatistic.Get<float>("heath"));
    }

    public void Heal(float reward)
    {
        playerStatistic.Health += reward;
        GlobalReference.AttemptInvoke(Events.HEALTH_CHANGED);
    }

    public void MultiplyMaxHealth(float reward) => playerStatistic.MaxHealth.AddMultiplier("reward", reward, true);

    public void IncreaseMaxHealth(float reward) => playerStatistic.MaxHealth.AddModifier("reward", reward);

    // If we go the event route this should change right?
    private void OnDeath()
    {
        Debug.Log("Player died", this);
    }
}
