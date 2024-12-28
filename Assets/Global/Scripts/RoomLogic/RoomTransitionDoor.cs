using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    [HideInInspector] public RoomType nextRoomType;
    public int nextRoomId;
    private int roomAmounts;

    private GameManagerReference gameManagerReference;
    private CrossfadeController crossfadeController;
    private DoorManager doorManager;

    private string currentScenename;
    private void Awake()
    {
        IsDisabled = IsDisabled; // ugly fix so maybe we have to change in the future
        GlobalReference.SubscribeTo(Events.ROOM_FINISHED, RoomFinished);
        crossfadeController = GlobalReference.GetReference<CrossfadeController>();
    }

    public void Initialize()
    {
        gameManagerReference = GlobalReference.GetReference<GameManagerReference>();
        doorManager = GlobalReference.GetReference<DoorManager>();
        roomAmounts = gameManagerReference.GetAmountForRoomType(nextRoomType);
        int randomIndex = Random.Range(1, roomAmounts + 1);
        nextRoomName = nextRoomType.ToString() + "_" + randomIndex;
    }

    private void RoomFinished()
    {
        IsDisabled = false;
    }

    public override void OnInteract(ActionMetaData _)
    {
        Debug.Log("OnTriggerEnter Happend");
        StartCoroutine(LoadNextRoom());
    }

    private IEnumerator LoadNextRoom()
    {
        yield return StartCoroutine(crossfadeController.Crossfade_Start());
        yield return StartCoroutine(LoadRoomCoroutine());
    }

    public IEnumerator LoadRoomCoroutine()
    {
        var player = GlobalReference.GetReference<PlayerReference>().Player;
        // // Debug.Log(SceneManager.GetActiveScene().name);
        // Debug.Log(GetNonBaseSceneName("BaseScene"));

        // CollectableSave saveData = new CollectableSave(GetNonBaseSceneName("BaseScene"));
        // List<string> calories = saveData.Get<List<string>>("calories");
        // if (saveData.Get<int>("crumbs") < player.playerStatistic.Crumbs)
        // {
        //     saveData.Set("crumbs", player.playerStatistic.Crumbs);
        // }
        
        // foreach (var secret in player.playerStatistic.Calories)
        // {
        //     Debug.Log("Secret: " + secret);
        //         calories.Add(secret);
        // }
        // saveData.Set("calories", calories);
        // Debug.Log("Saved Crumbs: " + saveData.Get<int>("crumbs"));
        // Debug.Log("Saved Calories: " + player.playerStatistic.Calories.Count);
        // Debug.Log("Saved Calories: " + saveData.Get<List<string>>("calories").Count);
        // Debug.Log("---------------------------------------------------------------------");
        // saveData.SaveAll();
        
        //to reset everything that was picked up
        player.playerStatistic.CaloriesCount = 0;
        player.playerStatistic.Calories.Clear();
        player.playerStatistic.Crumbs = 0;
        
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.name != "BaseScene" && scene.isLoaded)
            {
                currentScenename = scene.name;
            }
        }

        SceneManager.LoadScene(nextRoomName, LoadSceneMode.Additive);

        var gameManagerReference = GlobalReference.GetReference<GameManagerReference>();
        if (gameManagerReference != null)
        {
            doorManager.currentId = nextRoomId; 
            Room nextRoom = gameManagerReference.GetRoom(nextRoomId);
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

    private string GetNonBaseSceneName(string baseSceneName)
    {
        return Enumerable.Range(0, SceneManager.sceneCount)
            .Select(i => SceneManager.GetSceneAt(i))
            .FirstOrDefault(scene => scene.name != baseSceneName && scene.isLoaded).name ?? baseSceneName;
    }

    [ContextMenu("Unlock Door")] void UnlockDoorTest() => IsDisabled = false;
    [ContextMenu("Lock Door")] void LockDoorTest() => IsDisabled = true;
    [ContextMenu("Open Door")] void OpenDoorTest() => OpenDoorAnimation();
    [ContextMenu("Invoke room finish event")] void InvoteRoomFinishedEvent() => GlobalReference.AttemptInvoke(Events.ROOM_FINISHED);
}
