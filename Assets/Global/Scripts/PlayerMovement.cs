using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public bool showInGizmos = false;
    
    [Header("Movement")] 
    public float movementSpeed = 100f;
    public float modelRotationSpeed = 10f; // relevant for how snappy the gravity rotation is. And also for rotating to left and right
    // This rotation speed influences to much, we probalby really need a oriontation object. 
    // This in its basic form its just a transform. However, when you rotate, press D, you can move the velocity already 90 degress
    // and then lerp this vector to that velocity vector, and the camera attaches to that then ofcourse.
    // (you can also make it when you already have some speed, that the A and D button have less power basiccally)
    // other things:
    // - jump
    // - grafity that makes sence
    // - based on speed, a max slope 
    
    
    [Header("Custom Gravity")]
    public float gravityForce = 29.81f;
    public float gravityFloorCheckDistance  = 1.5f;
    private Vector3 _gravityDirection = Vector3.down;
    
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveDirection;
    
    private Rigidbody _rb;
    private Transform _playerTransform;
    
    private void Start()
    {
        this._rb = this.GetComponent<Rigidbody>();
        this._playerTransform = this.GetComponent<Transform>();
        this._rb.useGravity = false; 
        this._rb.freezeRotation = true;
    }

    private void Update()
    {
        this._horizontalInput = Input.GetAxis("Horizontal");
        this._verticalInput = Input.GetAxis("Vertical");
    }
    
    private void FixedUpdate()
    {
        this.MovePlayer();
        this.ApplyCustomGravity();
        this.RotatePlayer();
    }
    
    private void MovePlayer()
    {
        // Calculate movement direction based on the input and orientation
        this._moveDirection = this._playerTransform.forward * this._verticalInput + 
                             this._playerTransform.right * this._horizontalInput;
        this._moveDirection = this._moveDirection.normalized * this.movementSpeed;
  
        this._rb.AddForce(this._moveDirection, ForceMode.Acceleration);
    }
    
    private void ApplyCustomGravity()
    {
        // Here we cast a ray downwards from the player position
        // If it hits something, than the inverse of the normal of the hit is the gravity direction to make the player stick to that surface
        // if it doesnt hit anything, the direction returns back to straight down
        var position = this._playerTransform.position;
        if (Physics.Raycast(position, this._gravityDirection , out var hit,
                            this.gravityFloorCheckDistance ))
            this._gravityDirection = -hit.normal;
        else
            this._gravityDirection = Vector3.down;
        
        if (this.showInGizmos)
            Debug.DrawRay(position,this._gravityDirection, Color.red,  0f, false);

        // Now that we are also correctly rotated, we can apply the "new" gravity to the player.
        var gravity = this._gravityDirection.normalized * this.gravityForce;
        this._rb.AddForce(gravity, ForceMode.Acceleration);
    }

    
    private void RotatePlayer()
    {  
        // We want the players forward direction, aka, its velocity, however, that can also go down if you are falling.
        // So we project it on a plane to cancel out the y-ax velocity
        var projectedDirection = Vector3.ProjectOnPlane(this._rb.linearVelocity, this._gravityDirection);
        if (this.showInGizmos)
            Debug.DrawRay(this._playerTransform.position,projectedDirection, Color.cyan,  0f, false);
        
        // If there's enough movement to determine a forward direction, we rotate it according to the velocity direction, and its gravity
        if (projectedDirection.sqrMagnitude > 0.01f)
        {
            var targetRotation = Quaternion.LookRotation(projectedDirection, -this._gravityDirection);
            this._rb.rotation = Quaternion.Slerp(this._rb.rotation, targetRotation, this.modelRotationSpeed * Time.fixedDeltaTime);
        }
        else // However, if this magnitude is to low, we rotate the player only by its gravity, ignoring the direction it is facing
        {
            var gravityAlignedRotation = Quaternion.FromToRotation(this.transform.up, -this._gravityDirection) * this.transform.rotation;
            this._rb.rotation = Quaternion.Slerp(this._rb.rotation, gravityAlignedRotation, this.modelRotationSpeed * Time.fixedDeltaTime);
        }
    }
}
