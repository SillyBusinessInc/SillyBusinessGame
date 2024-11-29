
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BaseTail", menuName = "BaseTail")]
public class BaseTail : ScriptableObject
{
    public string Name;
    public List<Attack> groundCombo = new();
    public List<Attack> airCombo = new();

    [HideInInspector]
    public List<Attack> currentCombo = new();

}
