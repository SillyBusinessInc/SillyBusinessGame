using System.Collections;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public abstract void Start();

    public abstract IEnumerator SetStateIdle();

    public abstract string Name { get;}
    public float cooldown;
    public float damage;

    
}
