using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyPrefabCount
{
    public GameObject enemyPrefab; // The enemy type
    public int amount;              // Number of this type to spawn
}

[System.Serializable]
public class WavePart
{
    public List<EnemyPrefabCount> enemyPrefabs; // List to replace dictionary
    public int spawnArea; // Possible spawn areas for this wave
}


[CreateAssetMenu(fileName = "NewWave", menuName = "ScriptableObjects/Wave")]
public class Wave : ScriptableObject
{
    public List<WavePart> waveParts; // List of enemy types and counts
    public float interval = 1; // delay interfal for the enemies

}