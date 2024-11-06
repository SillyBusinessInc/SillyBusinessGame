using UnityEngine;

public class Player : MonoBehaviour
{
    public BaseMovement baseMovement;
    public float jumpforce = 10f;
    public float speed = 5f;
    private float horizontalInput;
    private float verticalInput;
    public Rigidbody playerRb;

    public bool isOnGround = true;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.left * Time.deltaTime * speed * verticalInput);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    public Player()
    {
        baseMovement = new Idle(this);
    }
    public void SetState(BaseMovement newState)
    {
        baseMovement = newState;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
