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
    public int nextRoomid;

    public CrossfadeController crossfadeController;
    public PlayerSpawnPoint playerSpawnPoint;
    public DoorManager doorManager;
    
    private string currentScenename;
 

    private void Awake()
    {
        IsDisabled = IsDisabled; // ugly fix so maybe we have to change in the future
        GlobalReference.SubscribeTo(Events.ROOM_FINISHED, RoomFinished);
    }

    public void Initialize(){
        base.Start();
        nextRoomName = "Scene_" + nextRoomid;
        // Debug.Log("RoomTransitionDoor Scene nextRoomid =  " + nextRoomid);       
        // Debug.Log("RoomTransitionDoor Scene nextRoomName =  " + nextRoomName);
    }

    private void RoomFinished()
    {
        IsDisabled = false;
    }

    public override void OnInteract()
    {
        Debug.Log("OnTriggerEnter Happend");
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

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextRoomName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        var gameManagerReference = GlobalReference.GetReference<GameManagerReference>();
        if (gameManagerReference != null)
        {
            Room nextRoom = gameManagerReference.Get(nextRoomid);
            if (nextRoom != null)
            {
                gameManagerReference.activeRoom = nextRoom;
                Debug.Log($"Active Room updated to: {nextRoom.id}, Type: {nextRoom.roomType}");
            }
            else
            {
                Debug.LogError($"Failed to find Room with ID: {nextRoomid}");
            }
        }
        else
        {
            Debug.LogError("GameManagerReference is null");
        }

        
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScenename);
        while (!unloadOperation.isDone)
        {
            yield return null; 
        }

        Scene newScene = SceneManager.GetSceneByName(nextRoomName);
        SceneManager.SetActiveScene(newScene);
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
