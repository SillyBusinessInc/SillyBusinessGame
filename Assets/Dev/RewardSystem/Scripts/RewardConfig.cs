using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RewardConfig", menuName = "Scriptable Objects/RewardConfig")]
public class RewardConfig : ScriptableObject
{
    [SerializeField] private Dictionary<RoomType, LootTable> _lootTables = new();
}
