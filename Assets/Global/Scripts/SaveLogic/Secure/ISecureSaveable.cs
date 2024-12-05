using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISecureSaveable
{
    string Id {get; set;}
    public void Set<S>(S newValue);
}

public class SecureSaveable<T> : ISecureSaveable {
    public T Value { get; set; }
    public string Id { get; set; }

    public SecureSaveable(string id, T defaultValue) {
        Id = id;
        Value = defaultValue;
    }

    public void Set<S>(S newValue) {
        if (newValue is T t) Value = t;
    }
}

#region serializable data

[Serializable]
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

    public void AddAllFromSecureSavable(ISecureSaveable saveable) {
        if (saveable is SecureSaveable<int> s_int) Add(saveable.Id, s_int.Value);
        else if (saveable is SecureSaveable<float> s_float) Add(saveable.Id, s_float.Value);
        else if (saveable is SecureSaveable<string> s_string) Add(saveable.Id, s_string.Value);
        else if (saveable is SecureSaveable<bool> s_bool) Add(saveable.Id, s_bool.Value);
        else if (saveable is SecureSaveable<Vector2> s_vector2) Add(saveable.Id, s_vector2.Value);
        else if (saveable is SecureSaveable<Vector3> s_vector3) Add(saveable.Id, s_vector3.Value);
        else if (saveable is SecureSaveable<Vector4> s_vector4) Add(saveable.Id, s_vector4.Value);
        else Debug.LogError($"you are trying to save all values in [{saveable.Id}] of type [{saveable.GetType()}] which is not a serializable saveable and will not be saved");
    }

    public void UpdateSecureSaveable(ISecureSaveable saveable) {
        if (saveable is SecureSaveable<int> s_int) s_int.Set(Get<int>(saveable.Id));
        else if (saveable is SecureSaveable<float> s_float) s_float.Set(Get<float>(saveable.Id));
        else if (saveable is SecureSaveable<string> s_string) s_string.Set(Get<string>(saveable.Id));
        else if (saveable is SecureSaveable<bool> s_bool) s_bool.Set(Get<bool>(saveable.Id));
        else if (saveable is SecureSaveable<Vector2> s_vector2) s_vector2.Set(Get<Vector2>(saveable.Id));
        else if (saveable is SecureSaveable<Vector3> s_vector3) s_vector3.Set(Get<Vector3>(saveable.Id));
        else if (saveable is SecureSaveable<Vector4> s_vector4) s_vector4.Set(Get<Vector4>(saveable.Id));
        else Debug.LogError($"you are trying to load all values in [{saveable.Id}] of type [{saveable.GetType()}] which is not a serializable saveable and cannot be loaded");
    }
}

#endregion