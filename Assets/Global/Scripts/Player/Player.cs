using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float jumpForce = 2f;
    public float airBornMovementFactor = 0.5f;
    public int doubleJumps = 1;
    public float glideDrag = 2f;
    public float dodgeRollSpeed = 10f;
    public float dodgeRollDuration = 1f;

    [Header("Stats")]
    public PlayerStatistic playerStatistic;
    
    [Header("Attack")]
    public float attackResettingTime = 2f;
    public float TailTurnSpeed = 40f;
    public BoxCollider TransformTail;

    [Header("References")]
    [FormerlySerializedAs("playerRb")]
    public Rigidbody rb;
    public Transform orientation;

    [HideInInspector] public int attackCounter;
    [HideInInspector] public bool isSlamming;
    [HideInInspector] public float activeAttackCooldown;

    [HideInInspector] public bool canDodgeRoll = true;
    [HideInInspector] public int currentJumps = 0;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public PlayerStates states;
    private StateBase currentState;

    public Healthbar healthBar;

    [Header("Debugging")]
    [SerializeField] private string currentStateName = "none";

    public PlayerInput inputActions;
    // private PlayerInputActions inputActions;


    void Start()
    {
        states = new PlayerStates(this);
        SetState(states.Idle);
        inputActions = GetComponent<PlayerInput>();
        healthBar.UpdateHealthBar(0f, playerStatistic.maxHealth.GetValueRaw(), playerStatistic.health);
    }

    void Update()
    {
        currentState.Update();
        RotatePlayerObj();
        activeAttackCooldown = currentState.GetType().Name != "AttackingState" ? activeAttackCooldown + Time.deltaTime : 0.0f;
        if(activeAttackCooldown >= this.attackResettingTime)
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
        isGrounded = collision.gameObject.CompareTag("Ground");
        currentState.OnCollision(collision);
    }
    public void OnCollisionExit(Collision collision)
    {
        isGrounded = !collision.gameObject.CompareTag("Ground");
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
        Vector2 input = inputActions.actions["Move"].ReadValue<Vector2>();

        Vector3 moveDirection = orientation.forward * input.y + orientation.right * input.x;

        return moveDirection.normalized;
    }

    private void RotatePlayerObj()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            var direction = Vector3.ProjectOnPlane(rb.linearVelocity, Vector3.up).normalized;
            rb.MoveRotation(Quaternion.LookRotation(direction));
        }
    }

    // If we go the event route this should change right?
    public void OnHit(float damage)
    {
        var health = playerStatistic.health - damage;
        healthBar.UpdateHealthBar(0f, playerStatistic.maxHealth.GetValueRaw(), health);
        playerStatistic.health = health;
        if (health <= 0) OnDeath();
    }

    public void Heal(float reward)
    {
        var health = playerStatistic.health + reward;
        healthBar.UpdateHealthBar(0f, playerStatistic.maxHealth.GetValueRaw(), health);
        playerStatistic.health = health;
    }

    // If we go the event route this should change right?
    private void OnDeath()
    {
        Debug.Log("Player died", this);
    }
}
