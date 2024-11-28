using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DoorManager : Reference
{
    public List<int> connectedRooms = new List<int>(); 
    public List<GameObject> doors;
    private GameManagerReference gameManagerReference;
    private string lastSceneName;
    private RoomTransitionDoor roomTransitionDoor;

    public void Initialize()
    {
        gameManagerReference = GlobalReference.GetReference<GameManagerReference>();

        lastSceneName = GetNonBaseSceneName();
        if (!string.IsNullOrEmpty(lastSceneName))
        {
            doors = GameObject.FindGameObjectsWithTag("DoorPrefab").ToList();
            SetupDoors();
        }
        else
        {
            Debug.LogWarning("No non-BaseScene found!");
        }
    }

    void Update()
    {
        string currentSceneName = GetNonBaseSceneName();
        if (!string.IsNullOrEmpty(currentSceneName) && currentSceneName != lastSceneName)
        {
            Debug.Log($"Scene changed from {lastSceneName} to {currentSceneName}");
            lastSceneName = currentSceneName;
            doors = GameObject.FindGameObjectsWithTag("DoorPrefab").ToList();
            SetupDoors();
        }
    }

    private string GetNonBaseSceneName()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "BaseScene" && scene.isLoaded)
            {
                return scene.name;
            }
        }
        return null;
    }


    void LoadConnectedRooms()
    {
        connectedRooms = GetConnectedRoomIds(); 
        Debug.Log(string.Join(", ", connectedRooms));
        // Debug.Log("Connected Rooms : " + connectedRooms);   
        
    }

    void DeactivateExtraDoors()
    {
        int roomCount = connectedRooms.Count;

        if (roomCount < doors.Count)
        {
            int doorsToDeactivate = doors.Count - roomCount;
            for (int i = 0; i < doorsToDeactivate; i++)
            {
                int randomIndex;
                do{
                    randomIndex = Random.Range(0, doors.Count);
                } while (!doors[randomIndex].activeSelf);
                doors[randomIndex].SetActive(false);
            }
        }
    }

    void ConnectDoorsToRooms()
    {
        List<int> remainingRooms = new List<int>(connectedRooms);
        foreach (var doorObject in doors)
        {
            if (!doorObject.activeSelf) continue;

            RoomTransitionDoor door = doorObject.GetComponent<RoomTransitionDoor>();
            if (door == null) continue;

            int randomIndex = Random.Range(0, remainingRooms.Count);
            int selectedRoom = remainingRooms[randomIndex];

            door.nextRoomid = selectedRoom;
            // Debug.Log("DoorManager Script nextRoomid : " + door.nextRoomid);
            // Debug.Log("door:" + door.name + " connected to room " + selectedRoom);
            remainingRooms.RemoveAt(randomIndex);
        }
    }


    public List<int> GetConnectedRoomIds()
    {
        return gameManagerReference.GetNextRoomIds();
    }

    void SetupDoors()
    {
        LoadConnectedRooms();
        DeactivateExtraDoors();
        ConnectDoorsToRooms();
        doors.ForEach(x => x.GetComponent<RoomTransitionDoor>().Initialize());
    }

}
