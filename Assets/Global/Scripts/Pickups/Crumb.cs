using UnityEngine;

public class Crumb : MonoBehaviour
{
    public AudioClip pickupSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private bool isCollected = false;

    public void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player"))
        {
            isCollected = true;

            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            Destroy(gameObject, pickupSound.length);
        }
    }

}