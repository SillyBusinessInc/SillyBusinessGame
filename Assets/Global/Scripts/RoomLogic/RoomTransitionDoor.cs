using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class RoomTransitionDoor : Interactable
{
    [Header("Materials")]
    [SerializeField] private Material lockedMaterial;
    [SerializeField] private Material unlockedMaterial;
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private MeshRenderer doorMesh;
    [SerializeField] private string nextRoomName;
    private bool isTransitioning = false;
    public CrossfadeController crossfadeController;
    public PlayerSpawnPoint playerSpawnPoint;
    
    private string currentScenename;
 

    private void Awake()
    {
        GlobalReference.SubscribeTo(Events.ROOM_FINISHED, RoomFinished);
    }


    private void RoomFinished()
    {
        IsDisabled = false;
    }

    public override void OnInteract()
    {
        Debug.Log("OnTriggerEnter Happend");
        isTransitioning = true;
        StartCoroutine(LoadNextRoom());
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

    public override void OnDisableInteraction()
    {
        doorMesh.SetMaterials(new List<Material> { lockedMaterial });
    }

    public override void OnEnableInteraction()
    {
        doorMesh.SetMaterials(new List<Material> { unlockedMaterial });
    }

    private void OpenDoorAnimation()
    {
        if (IsDisabled) return;
        animator.SetTrigger("TriggerDoorOpen");
    }


    [ContextMenu("Unlock Door")]
    void UnlockDoorTest()
    {
        IsDisabled = false;
    }
    [ContextMenu("Lock Door")]
    void LockDoorTest()
    {
        IsDisabled = true;
    }
    [ContextMenu("Open Door")]
    void OpenDoorTest()
    {
        OpenDoorAnimation();
    }

    [ContextMenu("Invoke room finish event")]
    void InvoteRoomFinishedEvent()
    {
        GlobalReference.AttemptInvoke(Events.ROOM_FINISHED);
    }
}
