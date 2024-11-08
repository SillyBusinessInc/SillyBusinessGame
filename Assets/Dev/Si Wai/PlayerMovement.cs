
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public Transform orientation;
    public Rigidbody Rb;
    
    [Header("Movement")] 
    public float modelRotationSpeed = 0.2f;
    
    // Properties for state access
    // public Rigidbody Rb { get; private set; }
    public Vector3 ModelDownDirection { get; private set; } = Vector3.down;
    
    private void Start()
    {
    }

    private void Update()
    {
    }
    
    private void FixedUpdate()
    {
        this.RotatePlayerObject(this.ModelDownDirection,this.Rb.linearVelocity);
    }

    private void RotatePlayerObject(Vector3 downDirection, Vector3 forwardDirection)
    {
        // this.Rb.transform.position = orientation.GameObject. transform.position;
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
            var gravityAlignedRotation = Quaternion.FromToRotation(this.Rb.transform.up, -downDirection) * this.Rb.transform.rotation;
            this. Rb.rotation = Quaternion.Slerp(this.Rb.rotation, gravityAlignedRotation, this.modelRotationSpeed);
        }
    }
}