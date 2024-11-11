using System;
using System.Collections.Generic;
using UnityEngine;

// abstract record SaveHandler{
//     public record SaveableInt() : SaveHandler();
//     public record SaveableFloat() : SaveHandler();
//     public record SaveableString() : SaveHandler();
//     public record SaveableBool() : SaveHandler();

//     public (int, int, int) ToRGB(){
//         return this switch{
//             SaveableInt intS => (255,0,0),
//             SaveableFloat floatS => (0, 255, 0),
//             SaveableString stringS => (0,0,255),
//             _ => default
//         };
//     }

//     public void Save() {
//         Action<string> action = this switch {
//             SaveableInt intS => (255,0,0),
//             SaveableFloat floatS => (0, 255, 0),
//             SaveableString stringS => (0,0,255),
//             _ => default
//         };
//     }

//     private SaveHandler() {} // private constructor can prevent derived cases from being defined elsewhere
// }

// abstract class Saveable<T> {
//     public T value;

//     public T Load() {
//         // return value.GetType() switch {
//         //     typeof(string) => PlayerPrefs.GetString("test", ""),
//         // };

//         Dictionary<Type, Func<T>> result = new() {
//             { typeof(string), () => PlayerPrefs.GetString("test")},
//             { typeof(int), () => PlayerPrefs.GetInt("test")}
//         };
        
//         return result[value.GetType()]();
//     }
// }

interface ISaveable<T> {
    string Name { get; }
    T Data { get; set; }
    public void Save(T obj);
    public T Load();
}

class SaveableInt : ISaveable<int> {
    private readonly string name;
    private int data;
    public string Name { get => $"int_{name}"; }
    public int Data { 
        get => data;
        set => data = value;
    }
    public SaveableInt(string name_, int data_) {
        name = name_;
        data = data_;
    }
    public void Save(int obj) {
        PlayerPrefs.SetInt(Name, Data);
    }
    public int Load() {
        return PlayerPrefs.GetInt(Name);
    }
}