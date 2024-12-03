using System.Collections;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public Attack(string Name, float damage, float cooldown)
    {
        this.Name = Name;
        this.damage = damage;
        this.cooldown = cooldown;
    }

    public abstract void Start();

    public abstract IEnumerator SetStateIdle();

    public string Name;
    public float cooldown;
    public float damage;

}
