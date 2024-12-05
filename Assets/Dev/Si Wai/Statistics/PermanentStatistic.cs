using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class PermanentStatistic : BaseStatistic
{
    // using these classes to convert the list into json strings and back
    private class SerializableKeyValuePair {
        public string key;
        public float value;
    }

    private class DictionaryWrapper {
        public Dictionary<string, string> Dictionary;

        public DictionaryWrapper(Dictionary<string, string> dictionary) {
            Dictionary = dictionary;
        }
    }

    private class SerializationWrapper {
        public List<SerializableKeyValuePair> items;
    }

    // fields
    public readonly string Param;
    private readonly SaveSystem SaveSystem;
    public PermanentStatistic(string param, SaveSystem saveSystem) {
        Param = param;
        SaveSystem = saveSystem;
    }

    public string SerializeModifications() {
        Dictionary<string, string> JsonList = new() {
            {"baseMultipliers", ListToJson(baseMultipliers)},
            {"finalMultipliers", ListToJson(finalMultipliers)},
            {"modifiers", ListToJson(modifiers)}
        };

        return JsonUtility.ToJson(new DictionaryWrapper(JsonList));
    }

    // convert a list of KeyValuePair to JSON string
    private string ListToJson(List<KeyValuePair<string, float>> list) {
        List<SerializableKeyValuePair> serializableList = new();
        foreach (var kvp in list) { // json does not serialize KeyValuePair
            serializableList.Add(new SerializableKeyValuePair { key = kvp.Key, value = kvp.Value });
        }

        return JsonUtility.ToJson(new SerializationWrapper { items = serializableList });
    }

    public void DeserializeModifications(string jsonString) {
        if (!string.IsNullOrEmpty(jsonString)) {
            var wrapper = JsonUtility.FromJson<DictionaryWrapper>(jsonString);

            if (wrapper != null && wrapper.Dictionary != null) {
                if (wrapper.Dictionary.TryGetValue("baseMultipliers", out string baseMultipliersJson)) {
                    baseMultipliers = JsonToList(baseMultipliersJson);
                }
                if (wrapper.Dictionary.TryGetValue("finalMultipliers", out string finalMultipliersJson)) {
                    finalMultipliers = JsonToList(finalMultipliersJson);
                }
                if (wrapper.Dictionary.TryGetValue("modifiers", out string modifiersJson)) {
                    modifiers = JsonToList(modifiersJson);
                }
            }
        }
    }

    // converts JSON string back to a list of KeyValuePair
    private List<KeyValuePair<string, float>> JsonToList(string jsonString) {
        SerializationWrapper wrapper = JsonUtility.FromJson<SerializationWrapper>(jsonString);
        List<KeyValuePair<string, float>> list = new();
        foreach (var item in wrapper.items) {
            list.Add(new KeyValuePair<string, float>(item.key, item.value));
        }
        return list;
    }

    public new void AddModifier(string key, float modifier)
    {
        base.AddModifier(key, modifier);
        Save();
    }

    public new void AddMultiplier(string key, float multiplier, bool isBase)
    {
        base.AddMultiplier(key, multiplier, isBase);
        Save();
    }

    public new void RemoveModifier(string key)
    {
        base.RemoveModifier(key);
        Save();
    }

    public new void RemoveMultiplier(string key, bool isBase)
    {
        base.RemoveMultiplier(key, isBase);
        Save();
    }

    private void Save() {
        string json = SerializeModifications();
        SaveSystem.Set(Param, json);
        SaveSystem.SaveAll();
    }
}