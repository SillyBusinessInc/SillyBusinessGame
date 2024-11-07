using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public BaseMovement baseMovement;
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
        SetState(new Idle(this));
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            baseMovement.OnJump();
        }

        if (Keypressed())
        {
            baseMovement.OnWalk();
        }
        else
        {
            baseMovement.Still();
        }
    }

    public void SetState(BaseMovement newState)
    {
        baseMovement = newState;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetState(new Idle(this));
            isOnGround = true;
        }
    }

    Func<bool> Keypressed = () => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

    public void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.left * Time.deltaTime * speed * verticalInput);
    }
}
