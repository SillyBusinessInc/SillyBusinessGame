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
    [HideInInspector]
    public Rigidbody playerRb;
    [HideInInspector]
    public bool isOnGround = true;

    [Header("Debugging")]
    public string currentStateName;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        SetState(new IdleState(this));
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        currentState.Update();
        currentStateName = currentState.GetType().Name;
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollision(collision);
    }
    public void SetState(BaseState newState)
    {
        currentState = newState;
    }


}
