using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class EnemyPrefabCount
{
    public GameObject enemyPrefab; // The enemy type
    public int amount;              // Number of this type to spawn

    public static Dictionary<GameObject, int> GetDict(List<EnemyPrefabCount> list)
    {
        return list.ToDictionary(x => x.enemyPrefab, x => x.amount);
    }
}

[System.Serializable]
public class WavePart
{
    public List<EnemyPrefabCount> enemyPrefabs; // List to replace dictionary
    public int spawnArea; // Possible spawn areas for this wave
    public bool center = false; // Center the spawn area
}


[CreateAssetMenu(fileName = "NewWave", menuName = "ScriptableObjects/Wave")]
public class Wave : ScriptableObject
{
    public List<WavePart> waveParts; // List of enemy types and counts
    public float interval = 1; // delay interfal for the enemies

}