using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 250)]
    [SerializeField] private int maxHealth = 100; // this will likely be done through the stats class soon
    public float jumpForce = 2f;
    public float speed = 5f;
    public float airBornMovementFactor = 0.5f;
    public int doubleJumps = 1;
    public float glideDrag = 2f;
    public float dodgeRollSpeed = 10f;
    public float dodgeRollDuration = 1f;

    [Header("References")]
    [FormerlySerializedAs("playerRb")]
    public Rigidbody rb;
    public Transform orientation;
    public Healthbar healthBar;

    [HideInInspector] public bool canDodgeRoll = true;
    [HideInInspector] public int currentJumps = 0;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public PlayerStates states;
    private StateBase currentState;
    private float health; // this will likely be done through the stats class soon

    [Header("Debugging")]
    [SerializeField] private string currentStateName = "none";

    void Start()
    {
        states = new PlayerStates(this);
        SetState(states.Idle);
        health = maxHealth;
        healthBar.UpdateHealthBar(0f, maxHealth, health);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        currentState.Update();
        RotatePlayerObj();
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
        // go forward/back
        Vector3 forwardMovement = orientation.forward * verticalInput;

        // go left/right
        Vector3 rightMovement = Vector3.Cross(orientation.forward * horizontalInput, Vector3.down);

        return (forwardMovement + rightMovement).normalized;
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
        health -= damage;
        healthBar.UpdateHealthBar(0f, maxHealth, health);

        if (health <= 0) OnDeath();
    }

    // If we go the event route this should change right?
    private void OnDeath()
    {
        Debug.Log("Player died", this);
    }
}
