using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float jumpForce = 2f;
    public float airBornMovementFactor = 0.5f;
    public int doubleJumps = 1;
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
    public bool isGrounded;

    [HideInInspector]
    public bool tailCanDoDamage = false;

    [HideInInspector]
    public PlayerStates states;
    public StateBase currentState;

    [HideInInspector]
    public Vector2 movementInput;
    public Healthbar healthBar;

    [Header("Debugging")]
    [SerializeField]
    private string currentStateName = "none";

    // private PlayerInputActions inputActions;

    public float groundCheckDistance;
    private float bufferCheckDistance = 0.1f;

    void Start()
    {
        states = new PlayerStates(this);
        SetState(states.Idle);
        // health and maxHealth should be the same value at the start of game
        playerStatistic.health = playerStatistic.maxHealth.GetValue();
        if (healthBar)
            healthBar.UpdateHealthBar(
                0f,
                playerStatistic.maxHealth.GetValue(),
                playerStatistic.health
            );
    }

    void Update()
    {
        groundCheckDistance = rb.GetComponent<Collider>().bounds.extents.y;
        RaycastHit hit;
        Vector3 raycastPosition = new Vector3(rb.position.x, rb.position.y, rb.position.z);
        if (Physics.Raycast(raycastPosition, Vector3.down, out hit, groundCheckDistance))
        {
            if (!(hit.collider.gameObject.CompareTag("Player")))
            {
                if (Vector3.Angle(Vector3.up, hit.normal) < degreesToRotate)
                {
                    isGrounded = true;
                }
            }
        }
        else
        {
            isGrounded = false;
        }
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
        playerStatistic.health -= damage;
        if (healthBar != null)
            healthBar.UpdateHealthBar(
                0f,
                playerStatistic.maxHealth.GetValue(),
                playerStatistic.health
            );
        if (playerStatistic.health <= 0)
            OnDeath();
    }

    public void Heal(float reward)
    {
        playerStatistic.health += reward;
        healthBar.UpdateHealthBar(0f, playerStatistic.maxHealth.GetValue(), playerStatistic.health);
    }

    public void IncreaseMaxHealth(float reward)
    {
        playerStatistic.maxHealth.AddMultiplier("reward", reward, true);
        healthBar.UpdateHealthBar(0f, playerStatistic.maxHealth.GetValue(), playerStatistic.health);
    }

    // If we go the event route this should change right?
    private void OnDeath()
    {
        Debug.Log("Player died", this);
    }
}
