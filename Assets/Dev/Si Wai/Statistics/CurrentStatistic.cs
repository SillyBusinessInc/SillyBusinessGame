using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class CurrentStatistic : BaseStatistic
{
    [SerializeField]
    private float baseValue;  // Starting value, read-only
    private PermanentStatistic Perm;

    public CurrentStatistic(float bv, PermanentStatistic perm = null) {
        baseValue = bv;
        Perm = perm;
    }

    // Get the final value after applying modifiers
    public float GetValue()
    {
        float value = baseValue;
        // first, apply the base multipliers
        float baseMultiplier = baseMultipliers.Any() ? baseMultipliers.Sum(pair => pair.Value) : 1;
        float permanentBaseMultiplier = Perm?.PermanentBaseMulitpliers() ?? 1; // will be 1 if Perm is null
        value = value * baseMultiplier * permanentBaseMultiplier; // add current * permanent baseMultipliers
        
        // then, add the static modifiers
        modifiers.ForEach(pair => value += pair.Value);
        Perm?.PermanentModifiers().ForEach(pair => value += pair.Value);

        // lastly, combine the final multipliers
        float finalMultiplier = finalMultipliers.Any() ? finalMultipliers.Sum(pair => pair.Value) : 1;
        float permanentFinalMultiplier = Perm?.PermanentFinalMulitpliers() ?? 1;
        value = value * finalMultiplier * permanentFinalMultiplier;

        return value;
    }

    public int GetValueInt() => (int)MathF.Round(GetValue(), 0);
}