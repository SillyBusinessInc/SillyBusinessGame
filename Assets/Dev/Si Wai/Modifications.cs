using UnityEngine;
using TMPro;

public class Modifications : MonoBehaviour
{
    void Start() 
    {
        // this is for testing
        Debug.Log($"crumbs: {GlobalReference.PermanentPlayerStatistic.Get<int>("crumbs")}");

        GlobalReference.PermanentPlayerStatistic.Speed.AddModifier("speed", 1f);

        var jsonString = GlobalReference.PermanentPlayerStatistic.Get<string>("speed");
        GlobalReference.PermanentPlayerStatistic.Speed.DeserializeModifications(jsonString);
    }
}