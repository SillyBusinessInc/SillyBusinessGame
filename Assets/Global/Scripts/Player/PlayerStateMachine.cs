using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 2f;
    public float speed = 5f;
    public float airBornMovementFactor = 0.5f;
    public int doubleJumps = 1;
    public float glideDrag = 2f;
    
    public Rigidbody playerRb;
    public Transform orientation;
    
    [HideInInspector] public int currentJumps = 0;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public bool isGrounded;
    private StateBase _current;
    
    [Header("Debugging")]
    public string currentStateName;

    void Start()
    {
        // playerRb = GetComponent<Rigidbody>();
        SetState(new IdleState(this));
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        _current.Update();
        currentStateName = _current.GetType().Name;
        RotatePlayerObj();
    }
    
    void FixedUpdate() => _current.FixedUpdate();

    public void OnCollisionEnter(Collision collision)
    {
        isGrounded = collision.gameObject.CompareTag("Ground");
        _current.OnCollision(collision);
    }
    public void OnCollisionExit(Collision collision)
    {
        isGrounded = !collision.gameObject.CompareTag("Ground");
    }
    
    public void SetState(StateBase @new)
    {
        if ( _current!= null) 
            _current.Exit();
        _current = @new;
        _current.Enter();
    }

    public Vector3 GetDirection() {
        // go forward/back
        Vector3 forwardMovement = orientation.forward * verticalInput;

        // go left/right
        Vector3 rightMovement = Vector3.Cross(orientation.forward * horizontalInput, Vector3.down);

        return (forwardMovement + rightMovement).normalized;
    }

    private void RotatePlayerObj()
    {
        if (playerRb.linearVelocity.magnitude > 0.1f)
        { 
            var direction = Vector3.ProjectOnPlane(playerRb.linearVelocity, Vector3.up).normalized; 
            playerRb.MoveRotation(Quaternion.LookRotation(direction));
        }
    }
}
