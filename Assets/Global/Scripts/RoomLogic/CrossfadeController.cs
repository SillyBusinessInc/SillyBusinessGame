using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossfadeController : Reference
{
    public Animator animator;
    private float transitionTime = 1;

    void Start() {
        animator.SetTrigger("end");
        gameObject.SetActive(false);
    }

    public IEnumerator Crossfade_Start() {
        animator.SetTrigger("start");
        gameObject.SetActive(true);
        yield return new WaitForSeconds(transitionTime);
        gameObject.SetActive(false);
    }

    public IEnumerator Crossfade_End() {
        animator.SetTrigger("end");
        gameObject.SetActive(true);
        yield return new WaitForSeconds(transitionTime);
        gameObject.SetActive(false);
    }
}
