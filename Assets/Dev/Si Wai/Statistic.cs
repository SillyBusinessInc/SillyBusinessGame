using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.ComponentModel;

[Serializable]
public class Statistic
{
    [SerializeField]
    private float baseValue;  // Starting value, read-only

    // Lists of multipliers and modifiers with their associated keys
    private readonly List<KeyValuePair<string, DataObj>> baseMultipliers = new();
    private readonly List<KeyValuePair<string, DataObj>> finalMultipliers = new();
    private readonly List<KeyValuePair<string, DataObj>> modifiers = new();

    // Event to notify listeners about changes
    public event Action OnChange;

    public Statistic(float bv) {
        baseValue = bv;
    }

    // Get the final value after applying modifiers
    public float GetValue()
    {
        // prune all expired modifiers
        RemoveExpired();

        float value = baseValue;
        // first, apply the base multipliers
        float baseMultiplier = baseMultipliers.Any() ? baseMultipliers.Sum(pair => pair.Value.x) : 1;
       
        value *= baseMultiplier;
        
        // then, add the static modifiers
        modifiers.ForEach(pair => value += pair.Value.x);

        // lastly, combine the final multipliers
        float finalMultiplier = finalMultipliers.Any() ? finalMultipliers.Sum(pair => pair.Value.x) : 1;
        value *= finalMultiplier;

        return value;
    }

    public int GetValueInt() => (int)MathF.Round(GetValue(), 0);

    // Add new modifier  
    public void AddModifier(string key, float modifier, StatModificationMode mode = StatModificationMode.STACK, float duration = -1)
    {
        var pair = new KeyValuePair<string, DataObj>(key, new(modifier, duration));
        if (mode == StatModificationMode.LIMIT && modifiers.Contains(pair)) return;
        if (mode == StatModificationMode.OVERWRITE && modifiers.Contains(pair)) RemoveModifier(key);
        
        if (modifier != 0) {
            modifiers.Add(pair);
            TriggerOnChange();
        }
    }

    // Add new multiplier 
    public void AddMultiplier(string key, float multiplier, bool isBase, StatModificationMode mode = StatModificationMode.STACK, float duration = -1)
    {
        if (multiplier != 0)
        {
            var pair = new KeyValuePair<string, DataObj>(key, new(multiplier, duration));
            if (isBase){
                if (mode == StatModificationMode.LIMIT && baseMultipliers.Contains(pair)) return;
                if (mode == StatModificationMode.OVERWRITE && baseMultipliers.Contains(pair)) RemoveMultiplier(key, true);
                baseMultipliers.Add(pair);
            }
            else {
                if (mode == StatModificationMode.LIMIT && finalMultipliers.Contains(pair)) return;
                if (mode == StatModificationMode.OVERWRITE && finalMultipliers.Contains(pair)) RemoveMultiplier(key, false);
                finalMultipliers.Add(pair);
            }

            TriggerOnChange();
        }
    }

    // Remove a modifier by key
    public void RemoveModifier(string key)
    {
        if (modifiers.RemoveAll(pair => pair.Key == key) > 0)
            TriggerOnChange();
    }

    // Remove a multiplier by key
    public void RemoveMultiplier(string key, bool isBase)
    {
        bool removed = isBase
            ? baseMultipliers.RemoveAll(pair => pair.Key == key) > 0
            : finalMultipliers.RemoveAll(pair => pair.Key == key) > 0;

        if (removed) TriggerOnChange();
    }

    // Remove all expired modifiers and multipliers
    private void RemoveExpired() {
        modifiers.Where((x) => x.Value.endTime >= 0 && x.Value.endTime < Time.time).ToList().ForEach((x) => RemoveModifier(x.Key));
        baseMultipliers.Where((x) => x.Value.endTime >= 0 && x.Value.endTime < Time.time).ToList().ForEach((x) => RemoveMultiplier(x.Key, true));
        finalMultipliers.Where((x) => x.Value.endTime >= 0 && x.Value.endTime < Time.time).ToList().ForEach((x) => RemoveMultiplier(x.Key, false));
    }

    public bool HasModifier(string key) => modifiers.Where((x) => x.Key == key).Count() > 0;
    public bool HasBaseMultiplier(string key) => baseMultipliers.Where((x) => x.Key == key).Count() > 0;
    public bool HasFinalMultiplier(string key) => finalMultipliers.Where((x) => x.Key == key).Count() > 0;

    private void TriggerOnChange()
    {
        OnChange?.Invoke();
        GlobalReference.AttemptInvoke(Events.STATISTIC_CHANGED);
    }

    public void Subscribe(Action callback) => OnChange += callback;
    public void Unsubscribe(Action callback) => OnChange -= callback;

    private struct DataObj {
        public float x;
        public float endTime;
        public DataObj(float x_, float duration) {
            x = x_;
            if (duration >= 0) endTime = Time.time + duration;
            else endTime = -1;
        }
    }
}

public enum StatModificationMode {
    [Description("If upgrade already applied -> apply again")] STACK,
    [Description("If upgrade already applied -> don't apply")] LIMIT,
    [Description("If upgrade already applied -> replace old")] OVERWRITE
}