using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossfadeController : Reference
{
    public Animator animator;
    private float transitionTime = 1;

    void Strat() {
        animator.SetTrigger("end");
    }

    public IEnumerator Crossfade_Start() {
        animator.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
    }

    public IEnumerator Crossfade_End() {
        animator.SetTrigger("end");
        yield return new WaitForSeconds(transitionTime);
    }
}
