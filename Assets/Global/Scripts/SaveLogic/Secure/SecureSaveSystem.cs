using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework.Constraints;
using UnityEngine;

public abstract class SecureSaveSystem
{
    protected abstract string Prefix {get;}
    public abstract void Init();

    public SecureSaveSystem() {
        Init();
    }

    //Logic
    private string SavePath => Path.Combine(Application.persistentDataPath, $"{Prefix}.mold");
    private readonly Dictionary<string, ISecureSaveable> saveables = new() {};
    public bool IsDirty { get; private set; } = false;

    public T Get<T>(string id) {
        if (!saveables.ContainsKey(id)) {
            Debug.LogError($"{id} can not be found in {GetType().Name}");
            return default;
        }
        return (saveables[id] as SecureSaveable<T>).Value;
    }

    public void Set<T>(string id, T value) {
        if (!saveables.ContainsKey(id)) {
            Debug.LogError($"{id} can not be found in {GetType().Name}");
            return;
        }
        saveables[id].Set(value);
        IsDirty = true;
    }

    protected void Add<T>(string id, T defaultValue) {
        Type type = typeof(T);
        if (type != typeof(int) &&
            type != typeof(float) &&
            type != typeof(string) &&
            type != typeof(bool) &&
            type != typeof(Vector2) &&
            type != typeof(Vector3) &&
            type != typeof(Vector4)
        ) Debug.LogError($"cannot save [{id}] because [{type}] is not a securely saveable type. please only save int, float, string or bool");
        
        saveables.Add(id, new SecureSaveable<T>($"{Prefix}_{id}", defaultValue));
    }

    public void SaveAll() {
        BinaryFormatter formatter = new();
        using (FileStream stream = new(SavePath, FileMode.Create)) {
            SerializableData data = new();

            foreach (KeyValuePair<string, ISecureSaveable> saveable in saveables) {
                data.Add(saveable.Key, saveable.Value);
            }

            formatter.Serialize(stream, data);
            stream.Close();
        }
        IsDirty = false;
    }

    public void LoadAll() {
        if (File.Exists(SavePath)) {
            BinaryFormatter formatter = new();
            using (FileStream stream = new(SavePath, FileMode.Open)) {
                SerializableData data = (SerializableData)formatter.Deserialize(stream); // load into data element - then turn data element in saveables
                Debug.Log(data.Get<string>("test"));
                Debug.Log(data.Get<int>("test2"));
                Debug.Log(data.Get<bool>("test3"));
                Debug.Log(data.Get<Vector3>("test4"));

                foreach (KeyValuePair<string, ISecureSaveable> saveable in saveables) {
                    saveables[saveable.Key].Set(data.Get(saveable.Key));
                }

                Debug.Log(SavePath);
                stream.Close();
            }
            IsDirty = false;
        } 
        else {
            Debug.LogError($"you are trying to load file {SavePath} which doesn't exist on this device");
        }
    }

    // debug
    public void ListAll<T>() {
        foreach (KeyValuePair<string, ISecureSaveable> saveable in saveables) {
            Debug.Log((saveable.Value as SecureSaveable<T>).Value);
        }
    }
}
