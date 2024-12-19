using UnityEngine;
using System.Collections;

public class BreakableObjectWithAnimation : MonoBehaviour
{
    public float delay = 3f;     
    public float respawnDelay = 7f; 

    public bool canRespawn = true;
    public bool canBreak = true; 

    private Animator animator;
    private Collider boxcollider;
    private Renderer platformRenderer;
    private PlayerObject playerObject;

    void Start() {
        animator = GetComponent<Animator>();
        boxcollider = GetComponent<Collider>();
        platformRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision other) {
        animator.ResetTrigger("respawn");
        playerObject = GlobalReference.GetReference<PlayerReference>().PlayerObj;

        if (other.gameObject == playerObject.gameObject && canBreak) {
            Debug.Log("OnCollisionEnter");
            Invoke(nameof(CrackObject), delay);
        }
    }

    private void CrackObject()
    {
        animator.SetTrigger("cracking");
        Invoke(nameof(RespawnObject), respawnDelay);
    }


    private void RespawnObject()
    {
        animator.ResetTrigger("cracking");

        if (!canRespawn) {
            animator.enabled = false;
            boxcollider.enabled = false;
            platformRenderer.enabled = false;
        } else {
            animator.SetTrigger("respawn");
        }

    }
}
