using UnityEngine;

public class Tail : MonoBehaviour
{
    public Player player;

    public void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Tail hit enemy");
        }
    }
}
