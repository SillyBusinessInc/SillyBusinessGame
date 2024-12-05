using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISecureSaveable
{
    string Id {get; set;}
    public void Set<S>(S newValue);
    void Save();
    void Load();
}

public class SecureSaveable<T> : ISecureSaveable {
    public T Value { get; set; }
    public string Id { get; set; }

    public SecureSaveable(string id, T defaultValue) {
        Id = id;

        if (PlayerPrefs.HasKey(id)) Load();
        else Value = defaultValue;
    }

    public void Set<S>(S newValue) {
        if (newValue is T t) Value = t;
    }

    public void Save() {
        if (Value is int iValue) PlayerPrefs.SetInt(Id, iValue);
        if (Value is float fValue) PlayerPrefs.SetFloat(Id, fValue);
        if (Value is string sValue) PlayerPrefs.SetString(Id, sValue);
        if (Value is bool bValue) PlayerPrefs.SetInt(Id, bValue ? 1 : 0);
    }
    public void Load() {
        if (Value is int) Value = (T)(object)PlayerPrefs.GetInt(Id);
        if (Value is float) Value = (T)(object)PlayerPrefs.GetFloat(Id);
        if (Value is string) Value = (T)(object)PlayerPrefs.GetString(Id);
        if (Value is bool) Value = (T)(object)(PlayerPrefs.GetInt(Id) == 1);
    }
}

#region serializable saveable

[System.Serializable]
public class SerializableData {
    public Dictionary<string, int> ints = new();
    public Dictionary<string, float> floats = new();
    public Dictionary<string, string> strings = new();
    public Dictionary<string, bool> bools = new();
    public Dictionary<string, float[]> vectors = new();

    public void Add<T>(string id, T value) {
        if (value is int t_int) ints.Add(id, t_int);
        else if (value is float t_float) floats.Add(id, t_float);
        else if (value is string t_string ) strings.Add(id, t_string);
        else if (value is bool t_bool) bools.Add(id, t_bool);
        else if (value is Vector2 t_vector2) vectors.Add(id, new float[2] { t_vector2.x, t_vector2.y });
        else if (value is Vector3 t_vector3) vectors.Add(id, new float[3] { t_vector3.x, t_vector3.y, t_vector3.z });
        else if (value is Vector4 t_vector4) vectors.Add(id, new float[4] { t_vector4.x, t_vector4.y, t_vector4.z, t_vector4.w });
        else Debug.LogError($"you are trying to save [{id}] of type [{typeof(T)}] which is not a serializable type and will not be saved");
    }

    public T Get<T>(string id) {
        Type type = typeof(T);
        try {
            if (type == typeof(int)) return (T)(object)ints[id];
            if (type == typeof(float)) return (T)(object)floats[id];
            if (type == typeof(string)) return (T)(object)strings[id];
            if (type == typeof(bool)) return (T)(object)bools[id];
            if (type == typeof(Vector2)) return (T)(object) new Vector2(vectors[id][0], vectors[id][1]);
            if (type == typeof(Vector3)) return (T)(object) new Vector3(vectors[id][0], vectors[id][1], vectors[id][2]);
            if (type == typeof(Vector4)) return (T)(object) new Vector4(vectors[id][0], vectors[id][1], vectors[id][2], vectors[id][3]);
        }
        catch {
            Debug.LogError($"you are trying to load [{id}] of type [{type}] which does not exist, please check if the id and type are correct");
        }
        return default;
    }
}

#endregion