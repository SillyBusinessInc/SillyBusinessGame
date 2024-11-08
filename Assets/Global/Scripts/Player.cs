using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public BaseState currentState;
    public float jumpforce = 2f;
    public float speed = 5f;
    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;
    public Rigidbody playerRb;
    [HideInInspector]
    public bool isOnGround = true;
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

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollision(collision);
    }
    public void SetState(BaseState newState)
    {
        if ( currentState!= null) 
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public Vector3 getDirection() {
        // Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);
        // Vector3 normalized = direction.normalized;
        // return normalized;
        return orientation.forward;
    }
}
