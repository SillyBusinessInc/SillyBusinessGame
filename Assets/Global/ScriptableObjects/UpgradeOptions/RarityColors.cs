using System;
using UnityEngine;
[CreateAssetMenu(fileName = "RarityColors", menuName = "ScriptableObjects/RarityColors")]
public class RarityColors : ScriptableObject
{
    public RarityColorPair[] rarityColorPairs;

    [Serializable]
    public struct RarityColorPair
    {
        public int rarity; // acts like id
        public string name;
        public Color rarityColor;
        public Sprite rarityBackground;
    }
}
