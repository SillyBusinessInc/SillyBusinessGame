using UnityEngine;
using System.Collections;
public abstract class Attack : ScriptableObject {
    public abstract void Start();

    public abstract IEnumerator SetStateIdle();
    public abstract string Name { get; }
 }
