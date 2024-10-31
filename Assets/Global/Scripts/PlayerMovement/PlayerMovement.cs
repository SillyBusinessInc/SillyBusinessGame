using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 9f;
    public float modelRotationSpeed = 0.2f;
    public float fallingRotateSpeed = 0.2f;
    
    [Header("Jump")]
    public float airControl = 0.3f;
    public float jumpForce = 20f;
    [Tooltip("Percentage of world up direction to apply to jump direction. 0.0 = perpendicular to the floor, 1.0 = world up.")]
    public float jumpWorldUpPercentage = 0.7f;
    
    [Header("Custom Gravity")]
    public float gravityForce = 9.81f;
    public float gravityFloorCheckDistance = 1.5f;

    [Header("Debug")]
    public bool showGizmosLines;
    public string currentStateName; // not used, only here so you can see the state in the inspector
    
    // Properties for state access
    public Rigidbody Rb { get; private set; }
    public Vector3 GravityDirection { get; private set; } = Vector3.down;
    public Vector3 ModelDownDirection { get; private set; } = Vector3.down;
    public bool IsGrounded { get; private set; }
    public bool ShouldJump { get; private set; }
    
    // Internal properties, no real need to have these accessible from the states
    private PlayerStateMachine _stateMachine;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _isJumpHeld;
    private float _jumpTimer;

    private PlayerBaseState CurrentState => this._stateMachine.GetCurrentState();
    
    private void Start()
    {
        this.Rb = this.GetComponent<Rigidbody>();
        this.Rb.useGravity = false;
        this.Rb.freezeRotation = true;
        
        this._stateMachine = new(this);
        this._stateMachine.Initialize(PlayerStateType.Ground);
    }

    private void Update()
    {
        this._horizontalInput = Input.GetAxis("Horizontal");
        this._verticalInput = Input.GetAxis("Vertical");
        this.ShouldJump = Input.GetKey(KeyCode.Space);
        this._stateMachine.Update();
        
        this.currentStateName = this.CurrentState.GetType().Name;
    }
    
    private void FixedUpdate()
    {
        this.UpdateGroundCheck();
        this.ApplyCustomGravity();
        this._stateMachine.FixedUpdate();
        this.RotatePlayer(this.ModelDownDirection,this.Rb.linearVelocity);
    }
    
    private void UpdateGroundCheck()
    {
        var groundCheckData = this.CurrentState.DoGroundCheck();
        
        this.IsGrounded = groundCheckData.IsGrounded;
        this.GravityDirection = groundCheckData.GravityDirection;
        this.ModelDownDirection = groundCheckData.ModelDownDirection;

        if (this.showGizmosLines)
        {
            var p = this.transform.position;
            Debug.DrawRay(p, this.ModelDownDirection * this.gravityFloorCheckDistance, Color.yellow,0f, false);
            Debug.DrawRay(p, this.GravityDirection * this.gravityFloorCheckDistance, Color.red,0f, false);
        }
    }
    
    // It might very well be that this method should also be internalized in all the states.
    public Vector3 GetMoveDirection()
    {
        var t = this.transform;
        var direction = t.forward * this._verticalInput + t.right * this._horizontalInput;
        if(this.showGizmosLines)
            Debug.DrawRay(this.transform.position, direction, Color.cyan,0f, false);
        return direction;
    }
    
    private void ApplyCustomGravity()
    {
        var gravity = this.GravityDirection.normalized * (this.gravityForce * 10f);
        this.Rb.AddForce(gravity, ForceMode.Force);
    }

    private void RotatePlayer(Vector3 downDirection, Vector3 forwardDirection)
    {
        var projectedDirection = Vector3.ProjectOnPlane(forwardDirection, downDirection);
        // If the player is moving, rotate the model to face the direction of movement
        // If not (aka this magnitude is to low) then we only align the model with the down direction (gravity most of the time)
        // But the direction that we are looking at stays the same in that case
        
        if (projectedDirection.sqrMagnitude > 0.01f)
        { // rotate based on down & forward
            var targetRotation = Quaternion.LookRotation(projectedDirection, -downDirection);
            this.Rb.rotation = Quaternion.Slerp(this.Rb.rotation, targetRotation, this.modelRotationSpeed);
        }
        else 
        { // rotate only based on down
            var gravityAlignedRotation = Quaternion.FromToRotation(this.transform.up, -downDirection) * this.transform.rotation;
            this. Rb.rotation = Quaternion.Slerp(this.Rb.rotation, gravityAlignedRotation, this.modelRotationSpeed);
        }
    }
}