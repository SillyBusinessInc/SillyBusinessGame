using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using System.Linq;

public class CoinUI : MonoBehaviour
{
    private TextMeshProUGUI text; // Reference to the TextMeshProUGUI component
    private PlayerStatistic playerStats; // Reference to the player's statistics

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
        //coin collect
        if (text != null)
        {
            // Assuming `playerStats.Calories` holds the calories to display
            text.text = playerStats.Crumbs+"";
        }
    }
}
