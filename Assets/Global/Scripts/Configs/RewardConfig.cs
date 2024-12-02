using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RewardConfig", menuName = "Configs/RewardConfig")]
public class RewardConfig : ScriptableObject
{

    [System.Serializable]
    public class EventLootTablePair
    {
        public List<RoomType> roomType;
        public LootTable lootTable;
    }

    public List<EventLootTablePair> eventLootTablePairs;
    public LootTable fallBackLootTable;

    public LootTable GetLootTableForRoomType(RoomType roomType)
    {
        foreach (var pair in eventLootTablePairs)
        {
            if (pair.roomType.Contains(roomType))
            {
                return pair.lootTable;
            }
        }

        return fallBackLootTable;
    }
}
