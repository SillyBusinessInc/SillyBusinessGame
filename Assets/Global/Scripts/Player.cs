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
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        SetState(new IdleState(this));
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            currentState.OnJump();
        }

        if (Keypressed())
        {
            currentState.OnWalk();
        }
        else
        {
            currentState.Still();
        }
    }

    public void SetState(BaseState newState)
    {
        currentState = newState;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetState(new IdleState(this));
            isOnGround = true;
        }
    }

    Func<bool> Keypressed = () => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

}
