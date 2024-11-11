using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public Player player;
    void OnCollisionEnter(Collision collision)  {
        float groundCheckAngle = 60f;
        if (!collision.gameObject.CompareTag("Ground")) { // if the player touches something that is not the ground
            foreach (ContactPoint contact in collision.contacts) {
                float angle = Vector3.Angle(contact.normal, Vector3.up);
                
                if (angle <= groundCheckAngle) { // if players jumps on top of it
                    player.rb.linearVelocity = new Vector3(player.rb.linearVelocity.x, 0, player.rb.linearVelocity.z);
                    player.rb.AddForce(Vector3.up * player.jumpForce +  Vector3.forward * 2.0f, ForceMode.Impulse); // made the player jump again after touching
                    break;
                }
            }
        }
        player.OnCollisionEnter(collision);
    }
    
    void OnCollisionExit(Collision collision) => player.OnCollisionExit(collision);
}
