using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Newtonsoft.Json;

[Serializable]
public class PermanentStatistic : BaseStatistic
{
    // using these classes to convert the list into json strings and back
    private class KeyValue
    {
        public string Key { get; set; }
        public float Value { get; set; }
    }

    // fields
    public readonly string Param;
    private readonly SecureSaveSystem SaveSystem;
    public PermanentStatistic(string param, SecureSaveSystem saveSystem = null) {
        Param = param;
        SaveSystem = saveSystem;
    }

    public string SerializeModifications() {
        Dictionary<string, string> modifications = new() {
            {"baseMultipliers", ListToJson(baseMultipliers)},
            {"finalMultipliers", ListToJson(finalMultipliers)},
            {"modifiers", ListToJson(modifiers)}
        };

        return JsonConvert.SerializeObject(modifications);
    }

    // convert a list of KeyValuePair to JSON string
    string ListToJson(List<KeyValuePair<string, float>> list) => JsonConvert.SerializeObject(list);

    public void DeserializeModifications(string jsonString) {
        if (!string.IsNullOrEmpty(jsonString)) {
            Dictionary<string, string> modifications = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

            baseMultipliers = JsonToList(modifications["baseMultipliers"]);
            finalMultipliers = JsonToList(modifications["finalMultipliers"]);
            modifiers = JsonToList(modifications["modifiers"]);
        }
    }

    // converts JSON string back to a list of KeyValuePair
    List<KeyValuePair<string, float>> JsonToList(string jsonString) {
        var list = JsonConvert.DeserializeObject<List<KeyValue>>(jsonString);
        return list ? .Select(kv => new KeyValuePair<string, float>(kv.Key, kv.Value)).ToList();
    }

    // automatically saves when adding modifiers and multipliers
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
        SaveSystem?.Set(Param, SerializeModifications());
        SaveSystem?.SaveAll();
    }
}

[Serializable]
public class DictionaryWrapper {
    public Dictionary<string, string> Dictionary;

    public DictionaryWrapper(Dictionary<string, string> dictionary) {
        Dictionary = dictionary;
    }
}