using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeOption", menuName = "ScriptableObjects/UpgradeOption")]
public class UpgradeOption : ScriptableObject
{
    public Sprite image;
    public new string name;
    public string description;
}
