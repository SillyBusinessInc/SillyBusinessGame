using UnityEngine;

public class Crumb: MonoBehaviour
{
    //
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}