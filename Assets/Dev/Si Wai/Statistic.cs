using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class Statistic
{
    [SerializeField]
    private float baseValue;  // Starting value, read-only

    // Lists of multipliers and modifiers with their associated keys
    private List<KeyValuePair<string, float>> baseMultipliers = new();
    private List<KeyValuePair<string, float>> finalMultipliers = new();
    private List<KeyValuePair<string, float>> modifiers = new();

    // Get the final value after applying modifiers

    public Statistic(float bv) {
        baseValue = bv;
    }

    public float GetValue()
    {
        float value = baseValue;
        // first, apply the base multipliers
        float baseMultiplier = baseMultipliers.Any() ? baseMultipliers.Sum(pair => pair.Value) : 1;
       
        value *= baseMultiplier;
        
        // then, add the static modifiers
        modifiers.ForEach(pair => value += pair.Value);

        // lastly, combine the final multipliers
        float finalMultiplier = finalMultipliers.Any() ? finalMultipliers.Sum(pair => pair.Value) : 1;
        value *= finalMultiplier;

        return value;
    }

    public int GetValueInt() => (int)MathF.Round(GetValue(), 0);

    // Add new modifier  
    public void AddModifier(string key, float modifier)
    {
        if (modifier != 0)
            modifiers.Add(new KeyValuePair<string, float>(key, modifier));
    }

    // Add new multiplier 
    public void AddMultiplier(string key, float multiplier, bool isBase)
    {
        if (multiplier != 0)
        {
            var pair = new KeyValuePair<string, float>(key, multiplier);
            if (isBase)
                baseMultipliers.Add(pair);
            else
                finalMultipliers.Add(pair);
        }
    }

    // Remove a modifier by key
    public void RemoveModifier(string key)
    {
        modifiers.RemoveAll(pair => pair.Key == key);
    }

    // Remove a multiplier by key
    public void RemoveMultiplier(string key, bool isBase)
    {
        if (isBase)
            baseMultipliers.RemoveAll(pair => pair.Key == key);
        else
            finalMultipliers.RemoveAll(pair => pair.Key == key);
    }
}