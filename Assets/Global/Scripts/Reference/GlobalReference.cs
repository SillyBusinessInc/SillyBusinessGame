using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public static class GlobalReference
{
    // Non monobehavior singletons
    private static Statistics statistics;
    public static Statistics Statistics { 
        get => statistics != null ? statistics : statistics = new();
    }

    private static Settings settings;
    public static Settings Settings { 
        get => settings != null ? settings : settings = new();
    }

    private static GameManager gameManager;
    public static GameManager GameManager { 
        get => gameManager != null ? gameManager : gameManager = new();
    }

    // GameObject reference logic
    public static Dictionary<string, Reference> referenceList = new();

    public static void Register(Reference ref_) {
        if (!ref_) {
            Debug.LogWarning("Could not register object because no reference was given");
            return;
        }

        string name = ref_.GetType().Name;
        if (referenceList.ContainsKey(name)) {
            Debug.LogError($"{name} is already assigned and can not be assigned multiple times. Please ensure there is only one {name} in this scene");
            return;
        }

        referenceList.Add(name ,ref_);
    }

    public static void Unregister(Reference ref_) {
        if (!ref_) {
            Debug.LogWarning("Could not unregister object because no reference was given");
            return;
        }

        string name = ref_.GetType().Name;
        if (!referenceList.ContainsKey(name)) {
            Debug.LogWarning($"{name} cannot be unassigned because it has not been assigned in the first place");
            return;
        }

        referenceList.Remove(name);
    }

    /// <summary>
    /// <para>Finds the reference defined by the given generic type.</para>
    /// <para>Never use this method in Awake() or in OnDestroy()</para>
    /// </summary>
    public static T GetReference<T>() {
        string name = typeof(T).Name;
        if (referenceList[name] is T t) return t;
        return default;
    }
}
