using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    public float movementSpeed = 1f;
    public float rotationSpeed = 1f;
    public Transform orientation;
    
    private float horizontalInput;
    private float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    
    void Update()
    {
        MyInput();
    }
    
    void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput;
        moveDirection.y = 0;

        rb.AddForce(moveDirection.normalized * movementSpeed, ForceMode.VelocityChange);
        
        if (horizontalInput != 0)
        {
            orientation.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
    }
}
