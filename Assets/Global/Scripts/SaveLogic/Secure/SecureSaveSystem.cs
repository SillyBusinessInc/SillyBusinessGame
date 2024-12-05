using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SecureSaveSystem
{
    protected abstract string Prefix {get;}
    public abstract void Init();

    public SecureSaveSystem() {
        Init();
        LoadAll();
    }

    //Logic
    private string SavePath => Path.Combine(Application.persistentDataPath, $"{Prefix}.mold");
    private readonly Dictionary<string, ISecureSaveable> saveables = new() {};
    private readonly Dictionary<string, Type> saveableTypes = new() {};
    public bool IsDirty { get; private set; } = false;

    public T Get<T>(string id) {
        if (SerializableData.AllowedType(typeof(T)) == AllowedTypeReturnOption.ALLOWED_OBJECT) {
            return JsonUtility.FromJson<T>(Get<string>(id));
        }
        if (!saveables.ContainsKey(id)) {
            Debug.LogError($"{id} can not be found in {GetType().Name}");
            return default;
        }
        return (saveables[id] as SecureSaveable<T>).Value;
    }

    public void Set<T>(string id, T value) {
        if (SerializableData.AllowedType(typeof(T)) == AllowedTypeReturnOption.ALLOWED_OBJECT) {
            Set(id, JsonUtility.ToJson(value));
            return;
        }
        if (!saveables.ContainsKey(id)) {
            Debug.LogError($"{id} can not be found in {GetType().Name}");
            return;
        }
        saveables[id].Set(value);
        IsDirty = true;
    }

    protected void Add<T>(string id, T defaultValue) {
        Type type = typeof(T);
        AllowedTypeReturnOption result = SerializableData.AllowedType(type);
        if (result == AllowedTypeReturnOption.DISALLOWED) Debug.LogError($"cannot save [{id}] because [{type}] is not a securely saveable type");
        else if (result == AllowedTypeReturnOption.ALLOWED_OBJECT) {
            Add(id, JsonUtility.ToJson(defaultValue));
            return;
        }
        
        saveables.Add(id, new SecureSaveable<T>($"{Prefix}_{id}", defaultValue));
        saveableTypes.Add(id, typeof(T));
    }

    public void SaveAll() {
        BinaryFormatter formatter = new();
        using (FileStream stream = new(SavePath, FileMode.Create)) {
            SerializableData data = new();

            foreach (KeyValuePair<string, ISecureSaveable> saveable in saveables) {
                data.AddAllFromSecureSavable(saveable.Value);
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

                foreach (KeyValuePair<string, ISecureSaveable> saveable in saveables) {
                    data.UpdateSecureSaveable(saveable.Value);
                }
                
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
            if (saveable.Value is SecureSaveable<T> t) Debug.Log($"{t.Id}: {t.Value}");
        }
    }
}
