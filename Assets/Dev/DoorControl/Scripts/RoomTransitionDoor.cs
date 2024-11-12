using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomTransitionDoor : MonoBehaviour
{
    [SerializeField] private SceneAsset connectingRoom;
    [SerializeField] private Animator animator;
    private bool isLocked = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // disable this door when there is no connected room
        this.gameObject.SetActive(connectingRoom ? true : false);
    }

    void toggleLock(bool v)
    {
        isLocked = v;
    }

    void OpenDoor()
    {
        if (isLocked) return;
        animator.SetTrigger("TriggerDoorOpen");
    }

    // TODO: Remove these test functions on merge
    [ContextMenu("Open Door")]
    void OpenDoorTest()
    {
        OpenDoor();
    }
    [ContextMenu("Unlock Door")]
    void UnlockDoorTest()
    {
        toggleLock(false);
    }
    [ContextMenu("Lock Door")]
    void LockDoorTest()
    {
        toggleLock(true);
    }
}
