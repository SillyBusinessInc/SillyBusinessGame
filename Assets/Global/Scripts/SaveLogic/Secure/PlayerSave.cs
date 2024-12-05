using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSave : SecureSaveSystem
{
    protected override string Prefix => "testing";

    public override void Init() {
        Add("speed", 10);
        Add("location", new Vector3(10, 0, 10));
        Add("awesome", true);
        Add("lame", false);
        Add("tf?", 3.13509f);
        Add("spacy", new Vector4(315, -532, 43, -6365));
        Add("flat", new Vector2(10, 12));
        Add("data", new SerializableTest(1, "fabio", true));
    }
}

[Serializable]
public class SerializableTest {
    public int id;
    public string name;
    public bool awesome;

    public SerializableTest(int _id, string _name, bool _awesome) {
        id = _id;
        name = _name;
        awesome = _awesome;
    }

    public void Log() => Debug.Log($"id: {id}, name: {name}, awesome: {awesome}");
}