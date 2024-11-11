using UnityEngine;
using UnityEngine.Serialization;

public class PlayerObject : MonoBehaviour
{
    public Player player;
    private void OnCollisionEnter(Collision collision) => player.OnCollisionEnter(collision);

    private void OnCollisionExit(Collision collision) => player.OnCollisionExit(collision);
}
