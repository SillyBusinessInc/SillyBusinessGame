using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerReference : Reference
{
    public Table table;
    [SerializeField] private List<RoomAmountCombo> roomAmountCombo;

    void Start() {
        table = new();
        activeRoom = Get(0);
        GlobalReference.GetReference<DoorManager>().Initialize();
    }

    // Rooms
    public Room activeRoom;
    private readonly List<Room> rooms = new();
    public void Add(int id, RoomType roomType) {
        if (rooms.Where((x) => x.id == id).Count() == 0) rooms.Add(new(id, roomType));
    }
    public void Remove(int id) {
        Room room = rooms.Where((x) => x.id == id).FirstOrDefault();
        if (room != null) rooms.Remove(room); 
    }
    public Room Get(int id) => rooms.Where((x) => x.id == id).FirstOrDefault();
    public void Reset() => rooms.Clear();

    public List<Room> GetNextRooms() {
        if (table == null) {
            Debug.Log("table is null");
        }
        if (activeRoom == null) {
            Debug.Log("activeRoom is null");
        }
        var connectedIds = table.GetRow(activeRoom.id).branches;
        var connectedRooms = connectedIds.Select(id => Get(id)).Where(room => room != null).ToList();

        Debug.Log("Connected Rooms: " + string.Join(", ", connectedRooms.Select(r => r.roomType)));
        return connectedRooms;
    }

    public int GetAmountForRoomType(RoomType roomType)
    {
        RoomAmountCombo roomAmount = roomAmountCombo.Find(x => x.type == roomType);
        return roomAmount != null ? roomAmount.amount : 0;
    }
}

[System.Serializable]
public class RoomAmountCombo {
    public RoomType type;
    public int amount;
}