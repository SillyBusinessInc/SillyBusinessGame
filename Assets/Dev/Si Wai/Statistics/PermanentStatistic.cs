using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class PermanentStatistic : BaseStatistic
{
    public PermanentStatistic() {}
    public float PermanentBaseMulitpliers() => baseMultipliers.Any() ? baseMultipliers.Sum(pair => pair.Value) : 1;
    public float PermanentFinalMulitpliers() => finalMultipliers.Any() ? finalMultipliers.Sum(pair => pair.Value) : 1;
    public List<KeyValuePair<string, float>> PermanentModifiers() => modifiers;

    public List<string> ListWithModifications() {
        string multipliersList = ListToJson(baseMultipliers);
        string finalMultipliersList = ListToJson(finalMultipliers);
        string modifiersList = ListToJson(modifiers);
        return new List<string>(){ multipliersList, finalMultipliersList, modifiersList };
    }

    // convert a list of KeyValuePair to JSON
    public string ListToJson(List<KeyValuePair<string, float>> list) {
        List<SerializableKeyValuePair> serializableList = new();
        foreach (var kvp in list) { // json does not serialize KeyValuePair
            serializableList.Add(new SerializableKeyValuePair { key = kvp.Key, value = kvp.Value });
        }

        return JsonUtility.ToJson(new SerializationWrapper { items = serializableList });
    }
}

// using these classes to convert the list into json strings and back
public class SerializableKeyValuePair {
    public string key;
    public float value;
}

public class SerializationWrapper {
    public List<SerializableKeyValuePair> items;
}