using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossfadeController : MonoBehaviour
{
    public Animator animator;
    private float transitionTime;
    
    void Start() {
        animator.SetTrigger("end");
        transitionTime = 3/2;
    }

    public IEnumerator Crossfade() {
        animator.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
    }
}
