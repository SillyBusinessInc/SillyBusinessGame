using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private Animator animator;
    [SerializeField] private MeshRenderer doorMesh;
    [SerializeField] private string nextRoomName;
    
    private bool isLocked = true;
    private bool isTransitioning = false;
    public CrossfadeController crossfadeController;
    public PlayerSpawnPoint playerSpawnPoint;

    private string currentScenename;
 
    void Start()
    {
        toggleLock(true);
    }

    private void Awake() {
        GlobalReference.SubscribeTo(Events.ROOM_FINISHED, RoomFinished);
    }

    private void Update() {
    }

    private void RoomFinished() {
        toggleLock(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning && !isLocked) 
        {
            Debug.Log("OnTriggerEnter Happend");
            isTransitioning = true;
            StartCoroutine(LoadNextRoom());
        }
    }

    private IEnumerator LoadNextRoom()
    {
        yield return StartCoroutine(crossfadeController.Crossfade());
        yield return StartCoroutine(LoadRoomCoroutine());
    }

    public IEnumerator LoadRoomCoroutine()
    {

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.name != "BaseScene" && scene.isLoaded)
            {
                currentScenename = scene.name;
            }
        }

        GlobalReference.UnregisterReference(GlobalReference.GetReference<PlayerReference>());
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextRoomName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }


        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScenename);
        while (!unloadOperation.isDone)
        {
            yield return null; 
        }

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

    [ContextMenu("Invoke room finish event")]
    void InvoteRoomFinishedEvent()
    {
        GlobalReference.AttemptInvoke(Events.ROOM_FINISHED);
    }
}
