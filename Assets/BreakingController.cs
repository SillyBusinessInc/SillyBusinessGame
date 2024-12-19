using UnityEngine;

public class BreakingController : StateMachineBehaviour
{
    private Collider objectCollider;
    private Renderer objectRenderer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        objectCollider = animator.GetComponent<Collider>();
        objectRenderer = animator.GetComponent<Renderer>();
    }

    // polish the timing later

    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     float progress = stateInfo.normalizedTime;

    //     if (progress >= 0.5f && objectCollider != null && objectCollider.enabled)
    //     {
    //         objectCollider.enabled = false;
    //     }
    // }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        objectRenderer.enabled = false;
        // animator.enabled = false;
        objectCollider.enabled = false;
    }
}