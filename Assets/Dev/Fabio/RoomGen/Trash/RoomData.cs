// using System.Collections.Generic;
// using System.Linq;
// using System.Xml.Schema;
// using NUnit.Framework.Constraints;
// using UnityEngine;

// public class RoomData
// {
//     public IRoomType type;
//     public RoomCategory category;
// }

// public class RoomAllocationTable {
    
//     public static int nextId = 0;
//     public static int NextId { get => nextId++; }

//     public int seed = 1032312;
//     public Dictionary<int, RoomAllocationObject> table;

//     // settings
//     public int maxObjectPerDepth = 3;
//     public int maxBranchCount = 3;
//     public int targetDepth = 8;

//     public void Initialize() {
//         Random.InitState(seed);
//         table = new();
//         nextId = 0;

//         table.Add(NextId, new());
        
//     }

//     public void Generate(List<RoomAllocationObject> nextRooms, int nextDepth) {
//         foreach (RoomAllocationObject obj in nextRooms) {
//             obj.SetNextRooms(CalculateNextRooms(nextDepth));
//         }
//     }

//     public List<RoomAllocationObject> CalculateNextRooms(int targetDepth) {
//         int branchCount = Random.Range(1, maxBranchCount);
//         int newBranchCount = Random.Range(1, maxBranchCount - GetBranchCountByDepth(targetDepth));
//         int existingBranchCount = branchCount - newBranchCount;

//         List<RoomAllocationObject> res = new();

//         if (newBranchCount != 0) {
//             for (int i = 0; i < newBranchCount; i++) {
//                 RoomAllocationObject newRoom = new(targetDepth);

//                 table.Add(NextId, newRoom);
//                 res.Add(newRoom);
//             }
//         }

//         if (existingBranchCount != 0) {
//             List<KeyValuePair<int, RoomAllocationObject>> rooms = new(table.Where(x => x.Value.depth == targetDepth).ToList());
//             for (int i = 0; i < existingBranchCount; i++) {
//                 KeyValuePair<int, RoomAllocationObject> randomRoom = rooms[Random.Range(0, rooms.Count-1)];
//                 res.Add(randomRoom.Value);
//                 rooms.Remove(randomRoom);
//             }
//         }
//         return res;
//     }

//     public int GetBranchCountByDepth(int depth) {
//         return table.Where(x => x.Value.depth == depth).Count();
//     }
// }

// public struct RoomAllocationObject {
//     public int depth;
//     public RoomData roomData;
//     public List<RoomAllocationObject> nextRooms;

//     public RoomAllocationObject(int depth_) {
//         depth = depth_;
//         roomData = new();
//         nextRooms = new();
//     }

//     public void SetNextRooms(List<RoomAllocationObject> rooms) {
//         nextRooms = rooms;
//     }
// }

// public enum RoomCategory {
//     CARBS,
//     FRUIT,
//     VEGGIE,
//     DAIRY,
//     MEAT,
//     FAT
// }