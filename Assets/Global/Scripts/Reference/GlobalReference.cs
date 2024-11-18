using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Utilities;

public static class GlobalReference
{
    // Non monobehavior singletons
    // private static Statistics statistics;
    // public static Statistics Statistics { 
    //     get => statistics ??= new();
    // }

    // private static Settings settings;
    // public static Settings Settings { 
    //     get => settings ??= new();
    // }

    // GameObject reference logic
    public static Dictionary<string, Reference> referenceList = new();

    public static void RegisterReference(Reference ref_) {
        if (!ref_) {
            // Debug.LogWarning("Could not register object because no reference was given");
            return;
        }

        string name = ref_.GetType().Name;
        if (referenceList.ContainsKey(name)) {
            // Debug.LogError($"{name} is already assigned and can not be assigned multiple times. Please ensure there is only one {name} in this scene");
            return;
        }

        referenceList.Add(name ,ref_);
    }

    public static void UnregisterReference(Reference ref_) {
        if (!ref_) {
            // Debug.LogWarning("Could not unregister object because no reference was given");
            return;
        }

        string name = ref_.GetType().Name;
        if (!referenceList.ContainsKey(name)) {
            // Debug.LogWarning($"{name} cannot be unassigned because it has not been assigned in the first place");
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
        if (referenceList.ContainsKey(name) && referenceList[name] is T t) return t;
        return default;
    }

    // Event Logic
    public static Dictionary<Events, UnityEvent> eventList = new();

    public static void SubscribeTo(Events eventName, UnityAction action) {
        // Debug.Log($"Object Subscribed ({eventName})");
        TryGetEvent(eventName).AddListener(action);
    }

    public static void UnsubscribeTo(Events eventName, UnityAction action) {
        // Debug.Log($"Object Unsubscribed ({eventName})");
        TryGetEvent(eventName).RemoveListener(action);
    }

    public static void AttemptInvoke(Events eventName) {
        // Debug.Log($"Object Invoked ({eventName})");
        TryGetEvent(eventName).Invoke();
    }

    private static UnityEvent TryGetEvent(Events eventName) {
        return eventList.ContainsKey(eventName) ? eventList[eventName] : eventList[eventName] = new();
    }
}
