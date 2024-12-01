using System.Collections;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public abstract void Start();

    public abstract IEnumerator SetStateIdle();

    public string Name;
    public float cooldown;
    public float damage;

    
}
