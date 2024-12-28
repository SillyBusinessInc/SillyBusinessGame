using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class CaloriesUI : MonoBehaviour
{
    private TextMeshProUGUI text; // Reference to the TextMeshProUGUI component
    private PlayerStatistic playerStats; // Reference to the player's statistics
    private int caloriesCountExtra = 0;

    void Start()
    {
        //find by tag calories
        
        // Get the player's statistics from your player reference
        playerStats = GlobalReference.GetReference<PlayerReference>().Player.playerStatistic;

        // Get the TextMeshProUGUI component attached to this GameObject
        text = GetComponent<TextMeshProUGUI>();

        if (text == null)
        {
            Debug.LogWarning("TextMeshProUGUI component not found!");
        }
    }

    void Update()
    {
        //if the calories count is 0, set it to the amount of calories in the scene
        if (playerStats.CaloriesCount == 0)
        {
            playerStats.CaloriesCount = GameObject.FindGameObjectsWithTag("Calories").Count();
        }

        CollectableSave savedFile = null;
        
        // make a save file for the current scene where the object is in and not the base scene to make it unique
        if(GameObject.FindGameObjectsWithTag("Calories").FirstOrDefault() != null){
            savedFile = new CollectableSave(GameObject.FindGameObjectsWithTag("Calories").First().scene.name);
        }

        // Check if the amount of calories in the save file is less than the amount of calories the player has collected so far
        // this is done because those calories that are saved are destroyed or grayed out so they can't be collected again
        if (savedFile != null)
        {
            if (savedFile.Get<List<string>>("calories").Count() < playerStats.CaloriesCount)
            {
                caloriesCountExtra = savedFile.Get<List<string>>("calories").Count();
            }
        }
        

        // now add the calories to text count
        if (text != null)
        {
            // Assuming `playerStats.Calories` holds the calories to display
            text.text = caloriesCountExtra+playerStats.Calories.Count()+"/"+playerStats.CaloriesCount;
        }
    }
}
