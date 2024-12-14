using System;
using UnityEngine;
using Discord;
using UnityEngine.SceneManagement;

public class DiscordPresence : MonoBehaviour
{
    private Discord.Discord discord;
    private ActivityManager activityManager;

    private void Start()
    {
        InitializeDiscord();
        UpdatePresence();
    }

    private void InitializeDiscord()
    {
        const long applicationId = 1316852538561138738;
        discord = new Discord.Discord(applicationId, (ulong)Discord.CreateFlags.Default);
        activityManager = discord.GetActivityManager();
    }

    public void UpdatePresence()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string details;

        if (currentScene == "Menu" || currentScene == "Title" || currentScene == "Loading")
        {
            details = "Browsing the menus";
        }
        else
        {
            details = "In a run";
        }

        var activity = new Activity
        {
            Details = details,
            Timestamps =
            {
                Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            },
            Assets =
            {
                LargeImage = "game_icon",
                LargeText = "Game Icon Text"
            }
        };

        activityManager.UpdateActivity(activity, result =>
        {
            if (result == Discord.Result.Ok)
            {
                Debug.Log("Discord Rich Presence updated successfully.");
            }
            else
            {
                Debug.LogError($"Failed to update Discord Rich Presence: {result}");
            }
        });
    }

    private void Update()
    {
        discord.RunCallbacks();

        // Update presence if the scene changes
        if (SceneManager.GetActiveScene().name != lastSceneName)
        {
            lastSceneName = SceneManager.GetActiveScene().name;
            UpdatePresence();
        }
    }

    private void OnApplicationQuit()
    {
        discord.Dispose();
    }

    private string lastSceneName = "";
}
