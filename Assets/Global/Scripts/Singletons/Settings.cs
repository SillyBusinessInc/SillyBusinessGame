using UnityEngine;

public class Settings
{
    // Define the settings (only float, int or string)
    private float masterVolume;
    private int fullscreen;

    // Define how each setting is accessed
    public float MasterVolume {
        get => masterVolume;
        set => masterVolume = Mathf.Clamp(value, 0, 100);
    }
    public bool FullScreen {
        get => fullscreen == 1;
        set => fullscreen = value ? 1 : 0;
    }

    // Define how each setting is saved
    public void Save() {
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetInt("fullscreen", fullscreen);
    }

    // Define how each setting is loaded
    public void Load() {
        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        fullscreen = PlayerPrefs.GetInt("fullscreen");
    }
}