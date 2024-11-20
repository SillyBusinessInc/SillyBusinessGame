using System.Collections;
using UnityEditorInternal;
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
        Debug.Log("Start");
    }

    // public IEnumerator Crossfade_end() {
    //     animator.SetTrigger("end");
    //     yield return new WaitForSeconds(transitionTime);
    //     Debug.Log("End");
    // }
}
