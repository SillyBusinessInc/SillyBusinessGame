using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // health
    public Image healthbar;
    [Range(10, 1000)]
    public int maxHealth = 100;
    private float health;

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
    [HideInInspector]
    public Rigidbody playerRb;
    [HideInInspector]
    public bool isGrounded;

    [Header("Debugging")]
    public string currentStateName;

    void Start()
    {
        health = maxHealth;
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

    void FixedUpdate() => currentState.FixedUpdate();

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = collision.gameObject.CompareTag("Ground");
        currentState.OnCollision(collision);
    }
    private void OnCollisionExit(Collision collision)
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

    public void OnHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Debug.Log("PLAYER DIED", this);
    }
}
