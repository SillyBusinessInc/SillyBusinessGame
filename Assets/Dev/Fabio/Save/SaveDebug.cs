using System;
using UnityEngine;

/// <summary> Debug mono behaviour class for testing save system on Start() </summary>
public class SaveDebug : MonoBehaviour
{
    void Start()
    {
        DebugSaveSystem s = new();

        s.SaveAll();
        s.LoadAll();

        s.ListAll<int>();
        s.ListAll<float>();
        s.ListAll<string>();
        s.ListAll<bool>();
        s.ListAll<Vector2>();
        s.ListAll<Vector3>();
        s.ListAll<Vector4>();
        s.ListAll<DebugItem>();
    }
}

/// <summary> Debug SecureSaveSystem for testing save system functionality </summary>
public class DebugSaveSystem : SecureSaveSystem {
    protected override string Prefix => "debug_savesystem";
    public override void Init() {
        Add("int", 1);
        Add("float", 1.1f);
        Add("string", "string");
        Add("bool", true);
        Add("Vector2", new Vector2(1, 2));
        Add("Vector3", new Vector3(2, 3, 4));
        Add("Vector4", new Vector4(3, 4, 5, 6));
        Add("Item", new DebugItem(1, "yes"));
        Add("another_int", 10);
    }
}

/// <summary> Debug class exclusively for testing Serializable objects in SecureSaveSystem </summary>
[Serializable]
public class DebugItem {
    public int num;
    public string text;
    public DebugItem(int _num, string _text) {
        num = _num;
        text = _text;
    }
}