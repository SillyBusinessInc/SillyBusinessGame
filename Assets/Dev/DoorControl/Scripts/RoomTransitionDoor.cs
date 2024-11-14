using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


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
    private bool isTransitioning = false;
    public string nextRoomName; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // disable this door when there is no connected room
        this.gameObject.SetActive(connectingRoom ? true : false);
        toggleLock(true);
    }

    private void Awake() {
        GlobalReference.SubscribeTo(Events.ROOM_FINISHED, RoomFinished);
    }

    private void RoomFinished() {
        isLocked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning && isLocked) 
        {
            Debug.Log("OnTriggerEnter Happend");
            isTransitioning = true;
            LoadNextRoom();
        }
    }

    private void LoadNextRoom()
    {
        StartCoroutine(LoadRoomCoroutine());
    }

    private System.Collections.IEnumerator LoadRoomCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextRoomName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene);

        Scene newScene = SceneManager.GetSceneByName(nextRoomName);
        SceneManager.SetActiveScene(newScene);

        isTransitioning = false;
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
