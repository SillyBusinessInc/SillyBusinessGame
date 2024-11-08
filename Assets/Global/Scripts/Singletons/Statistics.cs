using UnityEngine;

public class Statistics
{
    // Define the statistic (only float, int or string)
    private float distanceWalked;
    private int timesJumped;

    // Define how each statistic is accessed
    public float DistanceWalked {
        get => distanceWalked;
        set => distanceWalked = Mathf.Clamp(value, 0, 100);
    }
    public bool TimesJumped {
        get => timesJumped == 1;
        set => timesJumped = value ? 1 : 0;
    }

    // Define how each statistic is saved
    public void Save() {
        PlayerPrefs.SetFloat("masterVolume", distanceWalked);
        PlayerPrefs.SetInt("fullscreen", timesJumped);
    }

    // Define how each statistic is loaded
    public void Load() {
        distanceWalked = PlayerPrefs.GetFloat("masterVolume");
        timesJumped = PlayerPrefs.GetInt("fullscreen");
    }
}