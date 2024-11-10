using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float jumpforce = 2f;
    public float speed = 5f;
    public int doubleJumps = 1;
    public float glideDrag = 2f;

    [HideInInspector]
    public BaseState currentState;
    [HideInInspector]
    public int currentJumps = 0;
    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;
    public Rigidbody playerRb;
    [HideInInspector]
    public bool isGrounded;
    public Transform orientation;

    [Header("Debugging")]
    public string currentStateName;

    public Transform TransformPlayer;
    public Transform TransformTail;
    public bool rotateLeft;

    public float turnSpeed;
    void Start()
    {
        // playerRb = GetComponent<Rigidbody>();
        SetState(new IdleState(this));
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        currentState.Update();
        currentStateName = currentState.GetType().Name;
        RotatePlayerObj();
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

    public void SetState(BaseState newState)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
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
        if (playerRb.linearVelocity.magnitude > 0.1f)
        {
            var direction = Vector3.ProjectOnPlane(playerRb.linearVelocity, Vector3.up).normalized;
            playerRb.MoveRotation(Quaternion.LookRotation(direction));
        }
    }
}
