using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class SaveSystem
{
    protected abstract string Prefix {get;}
    public abstract void Init();

    public SaveSystem() {
        Init();
    }

    //Logic
    private readonly Dictionary<string, ISaveable> saveables = new() {};

    public T Get<T>(string id) {
        if (!saveables.ContainsKey(id)) {
            Debug.LogError($"{id} can not be found in {GetType().Name}");
            return default;
        }
        return (saveables[id] as Saveable<T>).Value;
    }

    public void Set<T>(string id, T value) {
        if (!saveables.ContainsKey(id)) {
            Debug.LogError($"{id} can not be found in {GetType().Name}");
            return;
        }
        saveables[id].Set(value);
    }

    protected void Add<T>(string id, T defaultValue) {
        Type type = typeof(T);
        if (type != typeof(int) &&
            type != typeof(float) &&
            type != typeof(string) &&
            type != typeof(bool)
        ) Debug.LogError($"cannot save {id} because {type} is not a saveable type. please only save int, float, string or bool");
        
        saveables.Add(id, new Saveable<T>($"{Prefix}_{id}", defaultValue));
        
    }

    public void SaveAll() {
        foreach (KeyValuePair<string, ISaveable> saveable in saveables) {
            saveable.Value.Save();
        }
    }

    // debug
    public void ListAll<T>() {
        foreach (KeyValuePair<string, ISaveable> saveable in saveables) {
            Debug.Log((saveable.Value as Saveable<T>).Value);
        }
    }
}
