using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomTransitionDoor : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material lockedMaterial;
    [SerializeField] private Material unlockedMaterial;
    [Header("References")]
    [SerializeField] private SceneAsset connectingRoom;
    [SerializeField] private Animator animator;
    [SerializeField] private MeshRenderer doorMesh;

    private bool isLocked = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // disable this door when there is no connected room
        this.gameObject.SetActive(connectingRoom ? true : false);
        toggleLock(true);
    }

    void toggleLock(bool v)
    {
        isLocked = v;
        if (isLocked)
        {
            OnLock();
        }
        else
        {
            OnUnlock();
        }
    }

    void OnLock()
    {
        doorMesh.SetMaterials(new List<Material> { lockedMaterial });
    }

    void OnUnlock()
    {
        doorMesh.SetMaterials(new List<Material> { unlockedMaterial });
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
