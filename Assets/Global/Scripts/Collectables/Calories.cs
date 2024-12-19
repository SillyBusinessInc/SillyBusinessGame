using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Calories : Collectable
{
    public string caloriesId; // Unique identifier for this secret
    private CollectableSave saveData;

    void Awake()
    {
        saveData = new CollectableSave(SceneManager.GetActiveScene().name);

        List<string> calories = saveData.Get<List<string>>("calories");
        foreach (var secret in calories)
        {
            Debug.Log(secret);
        }
        if (calories.Contains(caloriesId))
        {
            Destroy(gameObject);
        }

    }

    public override void OnCollect()
    {
        GlobalReference.GetReference<PlayerReference>().Player.playerStatistic.Calories.Add(caloriesId);

        foreach (var secret in GlobalReference.GetReference<PlayerReference>().Player.playerStatistic.Calories)
        {
            Debug.Log(secret);
        }
        List<string> calories = saveData.Get<List<string>>("calories");
        calories.Add(caloriesId);
        saveData.Set("calories", calories);
    }
}
