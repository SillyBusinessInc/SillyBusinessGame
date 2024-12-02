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
    [HideInInspector]public RoomType nextRoomType;
    public int nextRoomId;
    private int roomAmounts;

    private PlayerSpawnPoint playerSpawnPoint;
    private DoorManager doorManager;
    private GameManagerReference gameManagerReference;
    private CrossfadeController crossfadeController;
    private int randomNum;
    
    private string currentScenename;
 

    private void Awake()
    {
        IsDisabled = IsDisabled; // ugly fix so maybe we have to change in the future
        GlobalReference.SubscribeTo(Events.ROOM_FINISHED, RoomFinished);
        crossfadeController = GlobalReference.GetReference<CrossfadeController>();
    }

    public void Initialize(){
        gameManagerReference = GlobalReference.GetReference<GameManagerReference>();
        roomAmounts = gameManagerReference.GetAmountForRoomType(nextRoomType);
        int randomIndex = Random.Range(1, roomAmounts+1);
        nextRoomName = nextRoomType.ToString() + "_" + randomIndex;

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
        yield return StartCoroutine(crossfadeController.Crossfade_Start());
        yield return StartCoroutine(LoadRoomCoroutine());
        yield return StartCoroutine(crossfadeController.Crossfade_End());
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
            Room nextRoom = gameManagerReference.Get(nextRoomId);
            if (nextRoom != null)
            {
                gameManagerReference.activeRoom = nextRoom;
            }
            else
            {
                Debug.LogError($"Failed to find Room with Type: {nextRoomType}");
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
