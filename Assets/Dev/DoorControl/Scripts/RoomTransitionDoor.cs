using System.Collections.Generic;
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
        LoadNextRoom();
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
