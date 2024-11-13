using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    private readonly string pre = "settings_";
    
    public float MasterVolume {
        get => PlayerPrefs.GetFloat($"{pre}masterVolume");
        set => PlayerPrefs.SetFloat($"{pre}masterVolume", Mathf.Clamp(value, 0f, 100f));
    }

    public bool FullScreen {
        get => PlayerPrefs.GetInt($"{pre}fullscreen") == 1;
        set => PlayerPrefs.SetInt($"{pre}fullscreen", value ? 1 : 0);
    }
}