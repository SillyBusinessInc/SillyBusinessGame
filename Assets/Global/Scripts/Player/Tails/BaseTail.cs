
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BaseTail", menuName = "BaseTail")]
public class BaseTail : ScriptableObject
{
    public List<GameObject> groundCombo = new();
    public List<GameObject> airCombo = new();

    [HideInInspector]
    public List<GameObject> currentCombo = new();

}
