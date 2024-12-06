using UnityEngine;
using TMPro;

public class Modifications : MonoBehaviour
{
    void Start() 
    {
        // this is for testing
        // Debug.Log($"crumbs: {GlobalReference.PermanentPlayerStatistic.Get<int>("crumbs")}");
        var speed = GlobalReference.PermanentPlayerStatistic.Get<PermanentStatistic>("speed");

        speed.AddModifier("speed", 1f);
        Debug.Log($"speed: {speed}");

        // var jsonString = GlobalReference.PermanentPlayerStatistic.Get<string>("speed");
        // speed.DeserializeModifications();
    }
}