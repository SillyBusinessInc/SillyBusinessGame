using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DoorManager : Reference
{
    private List<Room> connectedRooms = new List<Room>(); 
    private List<GameObject> doors;
    private GameManagerReference gameManagerReference;
    public int previousId=0;
    public int currentId=0;

    public void Initialize()
    {
        gameManagerReference = GlobalReference.GetReference<GameManagerReference>();
        doors = GameObject.FindGameObjectsWithTag("DoorPrefab").ToList();
        SetupDoors();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        if (currentId != 0 && currentId != previousId)
        {
            previousId = currentId;
            doors = GameObject.FindGameObjectsWithTag("DoorPrefab").ToList();
            SetupDoors();
        }
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
