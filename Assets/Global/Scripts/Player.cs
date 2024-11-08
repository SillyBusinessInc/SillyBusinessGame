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
        if ( currentState!= null) 
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public Vector3 getDirection() {
        return orientation.forward;
    }
}
