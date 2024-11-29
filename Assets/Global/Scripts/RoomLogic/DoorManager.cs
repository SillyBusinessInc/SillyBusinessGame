using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DoorManager : Reference
{
    private List<Room> connectedRooms = new List<Room>(); 
    private List<GameObject> doors;
    private GameManagerReference gameManagerReference;
    private string lastSceneName;

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
        connectedRooms = GetConnectedRooms(); 
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
        List<Room> remainingRooms = new List<Room>(connectedRooms);
        foreach (var doorObject in doors)
        {
            if (!doorObject.activeSelf) continue;

            RoomTransitionDoor door = doorObject.GetComponent<RoomTransitionDoor>();
            if (door == null) continue;

            int randomIndex = Random.Range(0, remainingRooms.Count);
            Room selectedRoom = remainingRooms[randomIndex];

            door.nextRoomType = selectedRoom.roomType;
            door.nextRoomId = selectedRoom.id;
            
            // set Bonus Room active false
            if(door.nextRoomType.ToString() == "BONUS") {
                doorObject.SetActive(false);
            }

            remainingRooms.RemoveAt(randomIndex);
        }
    }


    public List<Room> GetConnectedRooms()
    {
        return gameManagerReference.GetNextRooms();
    }

    void SetupDoors()
    {
        LoadConnectedRooms();
        DeactivateExtraDoors();
        ConnectDoorsToRooms();
        doors.ForEach(x => x.GetComponent<RoomTransitionDoor>().Initialize());
    }

}
