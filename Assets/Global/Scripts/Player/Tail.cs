using UnityEngine;

public class Tail : MonoBehaviour
{
    public Player player;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Onground");
        }
    }
}
