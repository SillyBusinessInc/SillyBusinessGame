using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    public float movementSpeed = 1f;
    public float rotationSpeed = 100f;
    public Transform orientation;
    
    [Header("Custom Gravity")]
    public float gravityForce = 9.81f;
    public Vector3 gravityDirection = Vector3.down;
    public float GravityFloorCheckDistance  = 2f;
    
    private float horizontalInput;
    private float verticalInput;
    Vector3 moveDirection;
    
    private Rigidbody rb;
    private Collider col;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rb.useGravity = false; 
        rb.freezeRotation = true;
    }
    
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    
    void FixedUpdate()
    {
        MovePlayer();
        ApplyCustomGravity();
    }
    
    private void MovePlayer()
    {
        // Calculate movement direction based on the input and orientation
        this.moveDirection = this.orientation.forward * this.verticalInput + 
                             this.orientation.right * this.horizontalInput;
        this.moveDirection = this.moveDirection.normalized * this.movementSpeed;
        
        // Apply force to move the player
        this.rb.AddForce(this.moveDirection, ForceMode.Acceleration);
    }
    
    private void ApplyCustomGravity()
    {
        var bounds = this.col.bounds;
        var position = bounds.center - new Vector3(0, bounds.extents.y, 0);
        
        // Cast a ray downwards from the player position
        // If it hits something, than the inverse of that normal is the gravity direction to make the player stick to that surface
        // if it doesnt hit anything, the direction returns back to straight down
        if (Physics.Raycast(position, this.gravityDirection , out var hit,
                            this.GravityFloorCheckDistance ))
            this.gravityDirection = -hit.normal;
        else
            this.gravityDirection = Vector3.down;
        
        // Calculate the target rotation so that "up" aligns with the inverse of gravityDirection
        // that way the player actually sticks to the surface
        var targetUp = -this.gravityDirection.normalized;
        var targetRotation = Quaternion.FromToRotation(this.transform.up, targetUp) * this.transform.rotation;
        this.rb.rotation = Quaternion.Slerp(this.rb.rotation, targetRotation, 
                                            this.rotationSpeed * Time.fixedDeltaTime);
        
        var gravity = this.gravityDirection.normalized * this.gravityForce;
        this.rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
