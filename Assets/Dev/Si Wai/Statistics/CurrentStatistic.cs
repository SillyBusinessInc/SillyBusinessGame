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
    private PermanentStatistic permanentStatistic;

    public CurrentStatistic(float bv, PermanentStatistic perm = null) {
        baseValue = bv;
        permanentStatistic = perm;
    }

    // Get the final value after applying modifiers
    public float GetValue()
    {
        float value = baseValue;
        // first, apply the base multipliers
        float baseMultiplier = BaseMultipliers();
        float permanentBaseMultiplier = permanentStatistic?.BaseMultipliers() ?? 1; // if Perm is null, it will be 1
        value = value * baseMultiplier * permanentBaseMultiplier; // add current * permanent baseMultipliers
        
        // then, add the static modifiers
        modifiers.ForEach(pair => value += pair.Value);
        permanentStatistic?.Modifiers().ForEach(pair => value += pair.Value);

        // lastly, combine the final multipliers
        float finalMultiplier = FinalMultipliers();
        float permanentFinalMultiplier = permanentStatistic?.FinalMultipliers() ?? 1;
        value = value * finalMultiplier * permanentFinalMultiplier;

        return value;
    }

    public int GetValueInt() => (int)MathF.Round(GetValue(), 0);
}