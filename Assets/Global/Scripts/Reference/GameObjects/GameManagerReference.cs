using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerReference : Reference
{
    public Table table;
    void Start() {
        table = new();
        activeRoom = Get(0);
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
    public List<int> GetNextRoomIds() => table.GetRow(activeRoom.id).branches;
}
