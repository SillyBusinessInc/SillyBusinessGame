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

[System.Serializable]
public class SerializableSaveable {
    public object Value;
}