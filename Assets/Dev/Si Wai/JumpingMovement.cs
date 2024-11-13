using UnityEngine;

public class JumpingMovement : MonoBehaviour
{
    public float bounceForceUp = 5.0f;
    public float bounceForceForward = 0.1f;
    public float topAngleThreshold = 105.0f;
    
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        
        if (rb) {
            foreach (ContactPoint contact in collision.contacts) {
                float angle = Vector3.Angle(contact.normal, Vector3.up);
                
                // if an object gets on top of the object this script is attached to
                if (angle >= topAngleThreshold) {
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                    rb.AddForce(transform.up * bounceForceUp +  rb.transform.forward * bounceForceForward, ForceMode.Impulse); // bounce effect
                    break;
                }
            }
        }
    }
}
