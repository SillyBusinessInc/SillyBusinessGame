using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using UnityEngine.UI;

// using System.Numerics;

public class Player : MonoBehaviour
{
    [Header("Walking Settings")]
    public float acceleration = 2;
    public float deceleration = 0.5f;
    public float currentMovementLerpSpeed = 100;

    [Header("Jumping Settings")]
    public float maxJumpHoldTime = 0.2f;
    public float airBorneMovementFactor = 0.5f;
    public float fallMultiplier = 7;
    public float jumpVelocityFalloff = 8;
    public float coyoteTime = 0.3f;

    [Header("Other Settings")]
    public float glideDrag = 2f;
    public float dodgeRollSpeed = 10f;
    public float dodgeRollDuration = 1f;
    public float dodgeRollDeceleration = 1f;
    public float groundCheckAngle = 50.0f;
    public float maxIdleTime = 20f;
    public float minIdleTime = 5f;


    [Header("Stats")]
    public PlayerStatistic playerStatistic = new();

    public Tail Tail;

    [Header("References")]
    [FormerlySerializedAs("playerRb")]
    public Rigidbody rb;
    public Transform orientation;

    [HideInInspector] public PlayerAnimationsHandler playerAnimationsHandler;
    [HideInInspector] public bool slamCanDoDamage = false;
    [HideInInspector] public int attackCounter;
    [HideInInspector] public int tailDoDamage;
    [HideInInspector] public bool isSlamming;
    [HideInInspector] public float activeAttackCooldown;
    [HideInInspector] public bool canDodgeRoll = true;
    [HideInInspector] public int currentJumps = 0;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public PlayerStates states;
    [HideInInspector] public StateBase currentState;
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public List<Collider> collidersEnemy;
    [HideInInspector] public float groundCheckDistance;
    [HideInInspector] public Vector3 targetVelocity;
    [HideInInspector] public float timeLeftGrounded;
    [HideInInspector] public float timeLastDodge;
    [HideInInspector] public float currentWalkingPenalty;
    [HideInInspector] public bool awaitingNewState = false;
    [HideInInspector] public Coroutine activeCoroutine;
    [HideInInspector] public float maxWalkingPenalty = 0.5f;

    [Header("Debugging")]
    [SerializeField] public bool isGrounded;
    [SerializeField] private string debug_currentStateName = "none";
    [SerializeField] private bool isKnockedBack = false;
    [HideInInspector] public Color debug_lineColor;
    [HideInInspector] public bool isHoldingJump = false;
    [HideInInspector] public bool isHoldingDodge = false;
    [HideInInspector] public bool isAttacking = false;
    // private PlayerInputActions inputActions;

    private bool IsLanding = false;
    [SerializeField] private Image fadeImage;


    void Start()
    {
        playerAnimationsHandler = GetComponent<PlayerAnimationsHandler>();
        states = new PlayerStates(this);
        SetState(states.Idle);
        // health and maxHealth should be the same value at the start of game
        collidersEnemy = new List<Collider>();

        playerStatistic.Health = playerStatistic.MaxHealth.GetValue();
        GlobalReference.AttemptInvoke(Events.HEALTH_CHANGED);
    }

    void Update()
    {
        GroundCheck();
        CheckLandingAnimation();
        currentState.Update();
        ApproachTargetVelocity();
        RotatePlayerObj();

        if (isGrounded) canDodgeRoll = true;
    }


    private void attackingAnimation() => isAttacking = true;
    private void attackingStoppedAnimation() => isAttacking = false;
    private void Awake()
    {
        GlobalReference.SubscribeTo(Events.PLAYER_ATTACK_STARTED, attackingAnimation);
        GlobalReference.SubscribeTo(Events.PLAYER_ATTACK_ENDED, attackingStoppedAnimation);
    }

    private void OnDestroy()
    {
        GlobalReference.SubscribeTo(Events.PLAYER_ATTACK_STARTED, attackingAnimation);
        GlobalReference.SubscribeTo(Events.PLAYER_ATTACK_ENDED, attackingStoppedAnimation);
    }
    
    private void OnDrawGizmos()
    {
        Debug.DrawLine(rb.position, rb.position + targetVelocity, debug_lineColor, 0, true);
    }

    void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(collision);
    }


    public void OnCollisionExit(Collision collision)
    {
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
                    if (Vector3.Angle(Vector3.up, hit.normal) < groundCheckAngle)
                    {
                        currentJumps = 0;
                        isGrounded = true;
                        playerAnimationsHandler.SetBool("IsOnGround", true);
                        return;
                    }
                }
            }
        }

        if (isGrounded)
        {
            isGrounded = false;
            IsLanding = false;
            timeLeftGrounded = Time.time;
            playerAnimationsHandler.SetBool("IsOnGround", false);
        }
        // playerAnimationsHandler.SetBool("IsOnGround", false);

    }
    private void CheckLandingAnimation()
    {
        if (rb.linearVelocity.y < -0.1f && isGrounded)
        {
            if (!IsLanding)
            {
                IsLanding = true;
                playerAnimationsHandler.resetStates();
                playerAnimationsHandler.animator.SetTrigger("IsLanding");
            }
        }
    }
    
    public void SetState(StateBase newState)
    {
        // stop active coroutine
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        // chance state
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();

        // storing current name for debugging
        debug_currentStateName = currentState.GetType().Name;
        debug_lineColor = Color.yellow;
    }

    public IEnumerator SetStateAfter(StateBase newState, float time, bool override_ = false)
    {
        // stop active coroutine
        if (activeCoroutine != null)
        {
            if (override_)
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = null;
            }
            else yield break;
        }

        // set state after time
        yield return new WaitForSeconds(time);
        activeCoroutine = null;
        SetState(newState);
    }

    public Vector3 GetDirection()
    {
        Vector3 moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;
        return moveDirection.normalized;
    }

    private void RotatePlayerObj()
    {
        if (isKnockedBack && rb.linearVelocity.magnitude < 0.1f) isKnockedBack = false;
        if (isKnockedBack) return;
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            Vector3 direction = Vector3.ProjectOnPlane(rb.linearVelocity, Vector3.up).normalized;
            if (direction != Vector3.zero) rb.MoveRotation(Quaternion.LookRotation(direction));
        }
    }

    private void ApproachTargetVelocity()
    {
        // return if there is no target velocity to move towards | currently disabled as I'm investigating it's necessity
        // if (targetVelocity == Vector3.zero) return;
        
        

        // slowly move to target velocity
        Vector3 newVelocity = Vector3.MoveTowards(rb.linearVelocity, targetVelocity, currentMovementLerpSpeed * Time.deltaTime);

        // adjust speed when slowing down
        if (newVelocity.sqrMagnitude < rb.linearVelocity.sqrMagnitude)
        {
            // preserve y velocity
            float yVal = newVelocity.y;

            // apply deceleration
            Vector3 adjustedVelocity = newVelocity.normalized * (rb.linearVelocity.magnitude * (-0.01f * (currentState == states.DodgeRoll ? dodgeRollDeceleration : deceleration) + 1));
            if (adjustedVelocity.sqrMagnitude < newVelocity.sqrMagnitude) adjustedVelocity = newVelocity;

            // apply adjustment
            newVelocity = new(adjustedVelocity.x, yVal, adjustedVelocity.z);
        }

        // apply new velocity
        rb.linearVelocity = newVelocity;
    }

    // TO BE CHANGED ===============================================================================================================================
    // If we go the event route this should change right?
    public void OnHit(float damage)
    {
        playerStatistic.Health -= damage;
        GlobalReference.AttemptInvoke(Events.HEALTH_CHANGED);

        if (playerStatistic.Health <= 0) OnDeath();
    }

    public void applyKnockback(Vector3 knockback, float time)
    {
        //
        // TODO: Need to be written once we have reworked movement
        //
        isKnockedBack = true;
        rb.linearVelocity = knockback;
        StartCoroutine(KnockbackStunRoutine(time));
        // above is temporary
    }

    public void Heal(float reward)
    {
        playerStatistic.Health += reward;
        GlobalReference.AttemptInvoke(Events.HEALTH_CHANGED);
    }

    public void MultiplyMaxHealth(float reward) {
        playerStatistic.MaxHealth.AddMultiplier("reward", reward, true);
        Heal(1f);
    }
    
    public void IncreaseMaxHealth(float reward) {
        playerStatistic.MaxHealth.AddModifier("reward", reward);
        Heal(1f);
    }

    // If we go the event route this should change right?
    [ContextMenu("Die!!!!!")]
    private void OnDeath()
    {
        Debug.Log("Player died", this);
        MoveToMenu();
    }
    
    private void MoveToMenu() => UILogic.FadeToScene("Death", fadeImage, this);


    IEnumerator KnockbackStunRoutine(float time = 0.5f)
    {
        yield return new WaitForSecondsRealtime(time);
        isKnockedBack = false;
    }
}
