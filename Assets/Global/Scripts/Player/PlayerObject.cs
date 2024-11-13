using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public Player player;
    void OnCollisionEnter(Collision collision) => player.OnCollisionEnter(collision);
    
    void OnCollisionExit(Collision collision) => player.OnCollisionExit(collision);
}
