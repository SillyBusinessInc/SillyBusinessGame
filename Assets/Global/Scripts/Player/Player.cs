using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using System.Collections.Generic;
// using System.Numerics;
public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float airBorneMovementFactor = 0.5f;
    public float glideDrag = 2f;
    public float dodgeRollSpeed = 10f;
    public float dodgeRollDuration = 1f;
    public float degreesToRotate = 50.0f;
    public float acceleration = 10;

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
    public Healthbar healthBar;

    [HideInInspector] public bool slamCanDoDamage = false;
    [HideInInspector] public int attackCounter;
    [HideInInspector] public int tailDoDamage;
    [HideInInspector] public bool isSlamming;
    [HideInInspector] public float activeAttackCooldown;
    [HideInInspector] public bool canDodgeRoll = true;
    [HideInInspector] public int currentJumps = 0;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public bool tailCanDoDamage = false;
    [HideInInspector] public PlayerStates states;
    [HideInInspector] public StateBase currentState;
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public List<Collider> collidersEnemy;
    [HideInInspector] public float groundCheckDistance;
    [HideInInspector] public Vector3 targetVelocity;

    [Header("Debugging")]
    [SerializeField] public bool isGrounded;
    [SerializeField] private string debug_currentStateName = "none";
    [SerializeField] private float debug_speed = 0;
    [SerializeField] private float debug_speedTarget = 0;
    [SerializeField] private float debug_speedDif = 0;
    // private PlayerInputActions inputActions;

    void Start()
    {
        states = new PlayerStates(this);
        SetState(states.Idle);
        // health and maxHealth should be the same value at the start of game
        collidersEnemy = new List<Collider>();
        playerStatistic.Health = playerStatistic.MaxHealth.GetValue();

        if (healthBar != null) healthBar.UpdateHealthBar();
    }

    void Update()
    {
        GroundCheck();
        currentState.Update();
        RotatePlayerObj();

        activeAttackCooldown = currentState != states.Attacking ? activeAttackCooldown + Time.deltaTime : 0.0f;

        if (activeAttackCooldown >= attackResettingTime)
        {
            attackCounter = 0;
            activeAttackCooldown = 0.0f;
        }

        if (isGrounded) canDodgeRoll = true;
        Debug.DrawLine(rb.position, rb.position + targetVelocity, Color.yellow, 1, true);
    }

    void FixedUpdate() {
        currentState.FixedUpdate();
        ApproachTargetVelocity();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isSlamming)
        {
            isSlamming = false;
            SetState(states.Idle);
        }
        currentState.OnCollisionEnter(collision);
    }

    public void OnCollisionExit(Collision collision) {
        currentState.OnCollisionExit(collision);
    }

    private void GroundCheck()
    {
        groundCheckDistance = rb.GetComponent<Collider>().bounds.extents.y;
        Vector3[] raycastOffsets = new Vector3[]
        {
            Vector3.zero, 
            new (0, 0, rb.GetComponent<Collider>().bounds.extents.z), 
            new (0, 0, -rb.GetComponent<Collider>().bounds.extents.z),
            new (rb.GetComponent<Collider>().bounds.extents.x, 0, 0), 
            new (-rb.GetComponent<Collider>().bounds.extents.x,0,0) ,
        };

        foreach (Vector3 offset in raycastOffsets)
        {
            Vector3 raycastPosition = rb.position + offset;
            if (Physics.Raycast(raycastPosition, Vector3.down, out RaycastHit hit, groundCheckDistance))
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

        // storing current name for debugging
        debug_currentStateName = currentState.GetType().Name;
    }

    public Vector3 GetDirection()
    {
        Vector3 moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;
        return moveDirection.normalized;
    }

    private void RotatePlayerObj()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            var direction = Vector3.ProjectOnPlane(rb.linearVelocity, Vector3.up).normalized;
            if (direction != Vector3.zero) rb.MoveRotation(Quaternion.LookRotation(direction));
        }
    }

    private void ApproachTargetVelocity() {
        Vector3 dif = targetVelocity - rb.linearVelocity;
        Vector3 movement = dif * acceleration;
        // accelerate
        // if (dif > 0) {
        //     movement = dif * acceleration;
        // }
        // // decelerate
        // else {
        //     movement = dif * acceleration;
        // }

        // rb.AddForce(GetDirection() * movement, ForceMode.Force);
        rb.linearVelocity = movement;

        debug_speedDif = dif.magnitude;
        debug_speed = rb.linearVelocity.magnitude;
        debug_speedTarget = targetVelocity.magnitude;
    }

    // TO BE CHANGED ===============================================================================================================================
    // If we go the event route this should change right?
    public void OnHit(float damage)
    {
        playerStatistic.Health -= damage;

        if (healthBar != null) healthBar.UpdateCurrentHealth();

        if (playerStatistic.Health <= 0) OnDeath();
    }

    public void Heal(float reward)
    {
        playerStatistic.Health += reward;
        healthBar.UpdateCurrentHealth();
    }

    public void MultiplyMaxHealth(float reward)
    {
        playerStatistic.MaxHealth.AddMultiplier("reward", reward, true);
        healthBar.UpdateMaxHealth();
    }

    public void IncreaseMaxHealth(float reward)
    {
        playerStatistic.MaxHealth.AddModifier("reward", reward);
        healthBar.UpdateMaxHealth();
    }


    // If we go the event route this should change right?
    private void OnDeath()
    {
        Debug.Log("Player died", this);
    }
}
